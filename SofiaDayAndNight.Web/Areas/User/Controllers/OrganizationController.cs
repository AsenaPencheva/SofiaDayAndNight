using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Areas.User.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService organizationService;
        private readonly IMapper mapper;

        public OrganizationController(IOrganizationService organizationService, IMapper mapper)
        {
            this.organizationService = organizationService;
            this.mapper = mapper;
        }

        // GET: Organization/Organization
        public ActionResult ProfileDetails(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var organization = this.organizationService.GetByUsername(username);
            if (organization == null)
            {
                return HttpNotFound();
            }

            var model = this.mapper.Map<OrganizationViewModel>(organization);

            model.OrganizationStatus = this.organizationService.GetStatus(this.User.Identity.GetUserId(), model.Id);

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Submit(OrganizationViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                return View("ProfileForm", model);
            }

            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var organization = this.mapper.Map<Organization>(model);
            organization.CreatedOn = DateTime.Now;
            // add default photo
            if (upload != null && upload.ContentLength > 0)
            {
                var image = new Image
                {
                    Name = System.IO.Path.GetFileName(upload.FileName),
                    ContentType = upload.ContentType
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    image.Data = reader.ReadBytes(upload.ContentLength);
                }
                organization.ProfileImage = image;
            }

            organization.User = user;

            //this.individualService.Create(individual);
            user.Organization = organization;
            user.IsCompleted = true;
            System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Update(user);

            return RedirectToAction("ProfileDetails", new { area = "User", username = user.UserName });
        }

        [HttpPost]
        public ActionResult Follow(Guid? id)
        {
            this.organizationService.Follow(this.User.Identity.GetUserId(), id);

            if (Request.IsAjaxRequest())
            {
                var model = this.mapper.Map<OrganizationViewModel>(this.organizationService.GetById(id));
                model.OrganizationStatus = OrganizationStatus.IsFollowed;

                return this.PartialView("_OrganizationInfoPartial", model);
            }

            return this.RedirectToAction("ProfileDetails" ,"Individual", new { username = this.User.Identity.Name });
        }

        [HttpPost]
        public ActionResult Unfollow(Guid? id)
        {
            this.organizationService.Unfollow(this.User.Identity.GetUserId(), id);

            if (Request.IsAjaxRequest())
            {
                var model = this.mapper.Map<OrganizationViewModel>(this.organizationService.GetById(id));
                model.OrganizationStatus = OrganizationStatus.None;

                return this.PartialView("_OrganizationInfoPartial", model);
            }

            return this.RedirectToAction("ProfileDetails", "Individual", new { username = this.User.Identity.Name });
        }

        [AjaxOnly]
        public ActionResult EventsList(string username)
        {
            var passedEvents = this.organizationService.GetPassedEvents(username)
               .Select(x => this.mapper.Map<EventViewModel>(x)).ToList();

            var currentEvents = this.organizationService.GetCurrentEvents(username)
              .Select(x => this.mapper.Map<EventViewModel>(x)).ToList();

            var upcommingEvents = this.organizationService.GetUpcomingEvents(username)
            .Select(x => this.mapper.Map<EventViewModel>(x)).ToList();

            var model = new EventsListViewModel();
            model.PassedEvents = passedEvents;
            model.OngoingEvents = currentEvents;
            model.UpCommingEvents = upcommingEvents;

            return this.PartialView("_EventsListPartial", model);
        }

        [AjaxOnly]
        public ActionResult FollowersList(string username)
        {
            var model = new FriendsListViewModel();
            model.Username = username;
            model.Friends = this.organizationService.GetFollowers(username).Select(x => this.mapper.Map<IndividualViewModel>(x));

            return this.PartialView("_IndividualsListPartial", model);
        }
    }
}