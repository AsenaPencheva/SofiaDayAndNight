using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;
using SofiaDayAndNight.Web.Infrastructure;
using SofiaDayAndNight.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Areas.User.Controllers
{
    [Authorize]
    public class IndividualController : Controller
    {
        private readonly IIndividualService individualService;
        private readonly IImageService imageService;
        private readonly IMapper mapper;
        private readonly IPhotoHelper photoHelper;

        public IndividualController(IIndividualService individualService, IImageService imageService, IMapper mapper,IPhotoHelper photoHelper)
        {
            this.individualService = individualService;
            this.imageService = imageService;
            this.mapper = mapper;
            this.photoHelper = photoHelper;
        }

        public ActionResult ProfileDetails(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var individual = this.individualService.GetByUsername(username);
        
            if (individual == null)
            {
                return HttpNotFound();
            }

            var model = this.mapper.Map<IndividualViewModel>(individual);

            // error if ViewBag.UserId is null
            model.IndividualStatus = this.individualService.GetStatus(this.User.Identity.GetUserId(), model.Id);

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Submit(IndividualViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                return View("ProfileForm", model);
            }

            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var individual = this.mapper.Map<Individual>(model);
            individual.CreatedOn = DateTime.Now;
            if (upload != null && upload.ContentLength > 0)
            {
                var imageViewModel = this.photoHelper.UploadImage(upload);
               
                var image= this.mapper.Map<Image>(imageViewModel);
                individual.ProfileImage = image;
            }

            individual.User = user;

            //this.individualService.Create(individual);
            user.Individual = individual;
            user.IsCompleted = true;
            System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Update(user);

            return RedirectToAction("ProfileDetails", new { area = "User", username = user.UserName });
        }

        [HttpPost] 
        public ActionResult SendFriendRequest(string username)
        {
            this.individualService.SendFriendRequest(this.User.Identity.Name, username);

            if (Request.IsAjaxRequest())
            {
                var model = this.mapper.Map<IndividualViewModel>(this.individualService.GetByUsername(username));
                model.IndividualStatus = IndividualStatus.IsRequested;

                return this.PartialView("_IndividualInfoPartial", model);
            }

            return this.RedirectToAction("ProfileDetails", new { username = this.User.Identity.Name });
        }

        [HttpPost]
        public ActionResult CancelFriendRequest(string username)
        {
            this.individualService.CancelFriendRequest(this.User.Identity.Name, username);

            if (Request.IsAjaxRequest())
            {
                var model = this.mapper.Map<IndividualViewModel>(this.individualService.GetByUsername(username));
                model.IndividualStatus = IndividualStatus.None;

                return this.PartialView("_IndividualInfoPartial", model);
            }

            return this.RedirectToAction("ProfileDetails", new { username = this.User.Identity.Name });
        }

        [HttpPost]
        public ActionResult ConfirmFriendship(string username)
        {
            this.individualService.ConfirmFriendship(this.User.Identity.Name, username);

            if (Request.IsAjaxRequest())
            {
                var model = this.mapper.Map<IndividualViewModel>(this.individualService.GetByUsername(username));
                model.IndividualStatus = IndividualStatus.IsFriend;

                return this.PartialView("_IndividualInfoPartial", model);
            }

            return this.RedirectToAction("ProfileDetails", new { username = this.User.Identity.Name });
        }

        [HttpPost]
        public ActionResult CancelFriendship(string username)
        {
            this.individualService.RemoveFriendship(this.User.Identity.Name, username);

            if (Request.IsAjaxRequest())
            {
                var model = this.mapper.Map<IndividualViewModel>(this.individualService.GetByUsername(username));
                model.IndividualStatus = IndividualStatus.None;

                return this.PartialView("_IndividualInfoPartial", model);
            }

            return this.RedirectToAction("ProfileDetails", new { username= this.User.Identity.Name });
        }


        public ActionResult FriendsRequest(string username)
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action can be invoke only by AJAX call");
            }

            var friendsList = this.individualService.GetFriendsRequests(username)
                .Select(x => this.mapper.Map<IndividualViewModel>(x)).ToList();

            return this.PartialView("_RequestsListPartial", friendsList);
        }

        public ActionResult Friends(string username)
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action can be invoke only by AJAX call");
            }

            var friendsList = this.individualService.GetFriends(username)
                .Select(x => this.mapper.Map<IndividualViewModel>(x)).ToList();

            return this.PartialView("_FriendsListPartial", friendsList);
        }

        [AjaxOnly]
        public ActionResult EventsList(string username)
        {
            var passedEvents = this.individualService.GetPassedEvents(username)
               .Select(x => this.mapper.Map<EventViewModel>(x)).ToList();

            var model = new EventsListViewModel();
            model.PassedEvents = passedEvents;
            model.OngoingEvents= passedEvents;
            model.UpCommingEvents = passedEvents;

            return this.PartialView("_EventsListPartial", model);
        }
    }
}