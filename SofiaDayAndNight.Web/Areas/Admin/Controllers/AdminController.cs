using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SofiaDayAndNight.Data.Services.Contracts;
using AutoMapper;
using SofiaDayAndNight.Web.Areas.Admin.Models;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Web.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IIndividualService individualService;
        private IOrganizationService organizationService;
        private IEventService eventService;
        private IMapper mapper;

        public AdminController(IIndividualService individualService,IOrganizationService organizationService,IEventService eventService,IMapper mapper)
        {
            this.individualService = individualService;
            this.organizationService = organizationService;
            this.eventService = eventService;
            this.mapper = mapper;
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            var individuals = this.individualService.GetAll();
           
            var organizations = this.organizationService.GetAll();
            var events = this.eventService.GetAll();

            var model = new DashboardViewModel();
            model.Individuals = individuals.Select(x => this.mapper.Map<IndividualViewModel>(x)).ToList();
            model.Organizations = organizations.Select(x => this.mapper.Map<OrganizationViewModel>(x)).ToList();
            model.Events = events.Select(x => this.mapper.Map<EventViewModel>(x)).ToList();

            return this.View("AllGrid",model);
        }

        //public ActionResult HierarchyBinding_Individuals([DataSourceRequest] DataSourceRequest request)
        //{
        //    var individuals = this.individualService.GetAll().Select(x => this.mapper.Map<IndividualViewModel>(x)).ToList();
        //    return Json(individuals.ToDataSourceResult(request));
        //}

        //public ActionResult HierarchyBinding_Organizations([DataSourceRequest] DataSourceRequest request)
        //{
        //    var individuals = this.organizationService.GetAll().Select(x => this.mapper.Map<OrganizationViewModel>(x)).ToList();
        //    return Json(individuals.ToDataSourceResult(request));
        //}

        public ActionResult HierarchyBinding_IndividualEvents(Guid id, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(this.individualService.GetById(id).Events
                .Select(x => this.mapper.Map<EventViewModel>(x)).ToList()
                .ToDataSourceResult(request));
        }

        public ActionResult HierarchyBinding_OrganizationEvents(Guid id, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(this.organizationService.GetById(id).Events
                .Select(x => this.mapper.Map<EventViewModel>(x)).ToList()
                .ToDataSourceResult(request));
        }


        //public ActionResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    return Json(productService.Read().ToDataSourceResult(request));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, IndividualViewModel individual)
        //{
        //    //if (individual != null && ModelState.IsValid)
        //    //{
        //    //    this..Create(product);
        //    //}

        //    return Json(new[] { individual }.ToDataSourceResult(request, ModelState));
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_UpdateIndividual([DataSourceRequest] DataSourceRequest request, IndividualViewModel individual)
        {
            if (individual != null && ModelState.IsValid)
            {
                var model = this.mapper.Map<Individual>(individual);
                this.individualService.Update(model);
            }

            return Json(new[] { individual }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_UpdateOrganization([DataSourceRequest] DataSourceRequest request, OrganizationViewModel organization)
        {
            if (organization != null && ModelState.IsValid)
            {
                var model = this.mapper.Map<Organization>(organization);
                this.organizationService.Update(model);
            }

            return Json(new[] { organization }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_UpdateEvent([DataSourceRequest] DataSourceRequest request, EventViewModel eventModel)
        {
            if (eventModel != null && ModelState.IsValid)
            {
                var model = this.mapper.Map<Event>(eventModel);
                this.eventService.Update(model);
            }

            return Json(new[] { eventModel }.ToDataSourceResult(request, ModelState));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, IndividualViewModel individual)
        //{
        //    if (individual != null && ModelState.IsValid)
        //    {
        //        var model = this.mapper.Map<Individual>(individual);
        //        this.individualService.Ban(model);
        //    }

        //    return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        //}
    }
}