using AutoMapper;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;
using System;
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
        private readonly IUserProvider userProvider;
        private readonly IPhotoHelper photoHelper;

        public OrganizationController(IOrganizationService organizationService, IMapper mapper, IPhotoHelper photoHelper, IUserProvider userProvider)
        {
            Guard.WhenArgument(organizationService, "organizationService").IsNull().Throw();
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();
            Guard.WhenArgument(photoHelper, "photoHelper").IsNull().Throw();

            this.organizationService = organizationService;
            this.mapper = mapper;
            this.userProvider = userProvider;
            this.photoHelper = photoHelper;
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

            model.OrganizationStatus = this.organizationService.GetStatus(this.User.Identity.Name, model.Id);

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Submit(OrganizationViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                return View("ProfileForm", model);
            }

            var user = this.userProvider.FindByName(User.Identity.Name);
            if (user == null)
            {
                return HttpNotFound();
            }

            var organization = this.mapper.Map<Organization>(model);
            organization.CreatedOn = DateTime.Now;
            // add default photo
            if (upload != null && upload.ContentLength > 0)
            {
                var imageViewModel = this.photoHelper.UploadImage(upload);

                var image = this.mapper.Map<Image>(imageViewModel);
                organization.ProfileImage = image;
            }

            organization.User = user;

            //this.individualService.Create(individual);
            user.Organization = organization;
            user.IsCompleted = true;
            this.userProvider.Update(user);

            return RedirectToAction("ProfileDetails", new { area = "User", username = user.UserName });
        }

        [HttpPost]
        public ActionResult Follow(Guid? id)
        {
            this.organizationService.Follow(this.User.Identity.Name, id);

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
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var followers = this.organizationService.GetFollowers(username).Select(x => this.mapper.Map<IndividualViewModel>(x)).ToList();

            return this.PartialView("_IndividualsListPartial", followers);
        }
    }
}