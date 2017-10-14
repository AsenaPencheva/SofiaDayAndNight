using System;
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
    public class EventController : Controller
    {
        private readonly IIndividualService individualService;
        private readonly IOrganizationService organizationService;
        private readonly IEventService eventService;
        private readonly IMapper mapper;
        private readonly IPhotoHelper photoHelper;

        public EventController(IEventService eventService,IIndividualService individualService,IOrganizationService organizationService, IMapper mapper, IPhotoHelper photoHelper)
        {
            Guard.WhenArgument(individualService, "individualService").IsNull().Throw();
            Guard.WhenArgument(organizationService, "organizationService").IsNull().Throw();
            Guard.WhenArgument(eventService, "eventService").IsNull().Throw();
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();
            Guard.WhenArgument(photoHelper, "photoHelper").IsNull().Throw();

            this.individualService = individualService;
            this.organizationService = organizationService;
            this.eventService = eventService;
            this.mapper = mapper;
            this.photoHelper = photoHelper;
        }

        [HttpGet]
        public ActionResult Create(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new EventViewModel();
            model.CreatorUserName = username;

            //TempData["creator"] = username;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EventViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var eventModel = this.mapper.Map<Event>(model);
            eventModel.CreatedOn = DateTime.Now;

            if (upload != null && upload.ContentLength > 0)
            {
                var image = this.photoHelper.UploadImage(upload);

                eventModel.Cover = this.mapper.Map<Image>(image);
            }

            var username = model.CreatorUserName;
            if (this.User.IsInRole(UserRole.Individual.ToString()))
            {
                this.individualService.CreateEvent(eventModel, username);
                return RedirectToAction("ProfileDetails", "Individual", new { area = "User", username = this.User.Identity.Name });
            }
            else if (this.User.IsInRole(UserRole.Organization.ToString()))
            {
                this.organizationService.CreateEvent(eventModel, username);
                return RedirectToAction("ProfileDetails", "Organization", new { area = "User", username = this.User.Identity.Name });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EventDetails(Guid? id)
        {
            var eventModel = this.eventService.GetById(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }

            var model = this.mapper.Map<EventViewModel>(eventModel);

            return this.View(model);
        }
        
    }
}