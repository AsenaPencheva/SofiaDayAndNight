using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;
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
    public class EventController : Controller
    {
        private readonly IIndividualService individualService;
        private readonly IOrganizationService organizationService;
        private readonly IEventService eventService;
        private readonly IMapper mapper;
        private readonly IPhotoHelper photoHelper;

        public EventController(IEventService eventService,IIndividualService individualService,IOrganizationService organizationService, IMapper mapper, IPhotoHelper photoHelper)
        {
            this.individualService = individualService;
            this.organizationService = organizationService;
            this.eventService = eventService;
            this.mapper = mapper;
            this.photoHelper = photoHelper;
        }
        [HttpGet]
        public ActionResult Create(string username)
        {
            var model = new EventViewModel();

            TempData["creatorId"] = username;
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

            var creatorId = TempData["creatorId"].ToString();
            if (this.User.IsInRole(UserRole.Individual.ToString()))
            {
                this.individualService.CreateEvent(eventModel, creatorId);
                return RedirectToAction("ProfileDetails", "Individual", new { area = "User", username = this.User.Identity.Name });
            }
            else if (this.User.IsInRole(UserRole.Organization.ToString()))
            {
                this.organizationService.CreateEvent(eventModel, Guid.Parse(creatorId));
                return RedirectToAction("ProfileDetails", "Organization", new { area = "User", username = this.User.Identity.Name });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EventDetails(Guid id)
        {
            var eventModel = this.eventService.GetById(id);
            var model = this.mapper.Map<EventViewModel>(eventModel);

            return this.View(model);
        }
        
    }
}