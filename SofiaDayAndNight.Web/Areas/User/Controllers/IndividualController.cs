using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using Bytes2you.Validation;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;

namespace SofiaDayAndNight.Web.Areas.User.Controllers
{
    [Authorize]
    public class IndividualController : Controller
    {
        private readonly IIndividualService individualService;
        //private readonly IImageService imageService;
        private readonly IMapper mapper;
        private readonly IPhotoHelper photoHelper;
        private readonly IUserProvider userProvider;

        public IndividualController(IIndividualService individualService, IMapper mapper, IPhotoHelper photoHelper, IUserProvider userProvider)
        {
            Guard.WhenArgument(individualService, "individualService").IsNull().Throw();
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();
            Guard.WhenArgument(photoHelper, "photoHelper").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();

            this.individualService = individualService;
            //this.imageService = imageService;
            this.mapper = mapper;
            this.photoHelper = photoHelper;
            this.userProvider = userProvider;
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
            model.IndividualStatus = this.individualService.GetStatus(this.User.Identity.Name, model.Id);

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Submit(IndividualViewModel model, HttpPostedFileBase upload)
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

            var individual = this.mapper.Map<Individual>(model);
            individual.CreatedOn = DateTime.Now;
            // add default photo
            if (upload != null && upload.ContentLength > 0)
            {
                var imageViewModel = this.photoHelper.UploadImage(upload);

                var image = this.mapper.Map<Image>(imageViewModel);
                individual.ProfileImage = image;
            }

            individual.User = user;

            //this.individualService.Create(individual);
            user.Individual = individual;
            user.IsCompleted = true;
            this.userProvider.Update(user);

            return RedirectToAction("ProfileDetails", new { area = "User", username = user.UserName });
        }

        [HttpPost]
        public ActionResult SendFriendRequest(Guid? id)
        {
            this.individualService.SendFriendRequest(this.User.Identity.Name, id);

            if (Request.IsAjaxRequest())
            {
                var model = this.mapper.Map<IndividualViewModel>(this.individualService.GetById(id));
                model.IndividualStatus = IndividualStatus.IsRequested;

                return this.PartialView("_IndividualInfoPartial", model);
            }

            return this.RedirectToAction("ProfileDetails", new { username = this.User.Identity.Name });
        }

        [HttpPost]
        public ActionResult CancelFriendRequest(Guid? id)
        {
            this.individualService.CancelFriendRequest(this.User.Identity.Name, id);

            if (Request.IsAjaxRequest())
            {
                var model = this.mapper.Map<IndividualViewModel>(this.individualService.GetById(id));
                model.IndividualStatus = IndividualStatus.None;

                return this.PartialView("_IndividualInfoPartial", model);
            }

            return this.RedirectToAction("ProfileDetails", new { username = this.User.Identity.Name });
        }

        [HttpPost]
        public ActionResult ConfirmFriendship(Guid? id)
        {
            this.individualService.ConfirmFriendship(this.User.Identity.Name, id);

            if (Request.IsAjaxRequest())
            {
                var model = this.mapper.Map<IndividualViewModel>(this.individualService.GetById(id));
                model.IndividualStatus = IndividualStatus.IsFriend;

                return this.PartialView("_IndividualInfoPartial", model);
            }

            return this.RedirectToAction("ProfileDetails", new { username = this.User.Identity.Name });
        }

        [HttpPost]
        public ActionResult CancelFriendship(Guid? id)
        {
            this.individualService.RemoveFriendship(this.User.Identity.Name, id);

            if (Request.IsAjaxRequest())
            {
                var model = this.mapper.Map<IndividualViewModel>(this.individualService.GetById(id));
                model.IndividualStatus = IndividualStatus.None;

                return this.PartialView("_IndividualInfoPartial", model);
            }

            return this.RedirectToAction("ProfileDetails", new { username = this.User.Identity.Name });
        }

        [AjaxOnly]
        public ActionResult FriendsRequest(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var friendsList = this.individualService.GetFriendsRequests(username)
                .Select(x => this.mapper.Map<IndividualViewModel>(x)).ToList();

            foreach (var friend in friendsList)
            {
                friend.IndividualStatus = IndividualStatus.HasRequest;
            }

            return this.PartialView("_FriendsListPartial", friendsList);
        }

        [AjaxOnly]
        public ActionResult Friends(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var friendsList = this.individualService.GetFriends(username)
                .Select(x => this.mapper.Map<IndividualViewModel>(x)).ToList();

            foreach (var friend in friendsList)
            {
                friend.IndividualStatus = IndividualStatus.IsFriend;
            }

            return this.PartialView("_FriendsListPartial", friendsList);
        }

        [AjaxOnly]
        public ActionResult EventsList(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var passedEvents = this.individualService.GetPassedEvents(username)
               .Select(x => this.mapper.Map<EventViewModel>(x)).ToList();

            var currentEvents = this.individualService.GetCurrentEvents(username)
              .Select(x => this.mapper.Map<EventViewModel>(x)).ToList();

            var upcommingEvents = this.individualService.GetUpcomingEvents(username)
            .Select(x => this.mapper.Map<EventViewModel>(x)).ToList();

            var model = new EventsListViewModel();
            model.PassedEvents = passedEvents;
            model.OngoingEvents = currentEvents;
            model.UpCommingEvents = upcommingEvents;

            return this.PartialView("_EventsListPartial", model);
        }

        [AjaxOnly]
        public ActionResult FollowingList(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var organizations = this.individualService.GetFollowingOrganization(username)
                .Select(x => this.mapper.Map<OrganizationViewModel>(x));

            foreach (var org in organizations)
            {
                org.OrganizationStatus = OrganizationStatus.IsFollowed;
            }
            
            return this.PartialView("_OrganizationsListPartial", organizations);
        }
    }
}