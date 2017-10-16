using AutoMapper;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;
using SofiaDayAndNight.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Controllers
{
    public class HomeController : Controller
    {
        private IOrganizationService organizationService;
        private IIndividualService individualService;
        private IEventService eventService;
        private IMapper mapper;

        public HomeController(IOrganizationService organizationService, IIndividualService individualService, IEventService eventService, IMapper mapper)
        {
            this.organizationService = organizationService;
            this.individualService = individualService;
            this.eventService = eventService;
            this.mapper = mapper;

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string searchTerm)
        {
            var currentDate = DateTime.Now;
            var model = new ResultViewModel();

            var events = this.eventService.GetEventsByName(searchTerm);
            model.EventsList = new EventsListViewModel();
            model.EventsList.PassedEvents = events.Where(x => x.Ends < currentDate)
               .Select(x => this.mapper.Map<EventViewModel>(x)).ToList();
            model.EventsList.OngoingEvents = events.Where(x => x.Begins < currentDate && currentDate < x.Ends)
                .Select(x => this.mapper.Map<EventViewModel>(x)).ToList();
            model.EventsList.UpCommingEvents = events.Where(x => currentDate < x.Begins)
                .Select(x => this.mapper.Map<EventViewModel>(x)).ToList();

            var individuals  = this.individualService.GetIndividualsByNameOrUsername(searchTerm).Select(x => this.mapper.Map<IndividualViewModel>(x)).ToList();
            foreach (var i in individuals)
            {
                i.IndividualStatus = this.individualService.GetStatus(User.Identity.Name, i.Id);
            }

            model.Individuals = individuals;

            var organizations=this.organizationService.GetPlacesByNameOrUsername(searchTerm).Select(x => this.mapper.Map<OrganizationViewModel>(x)).ToList();

            foreach (var o in organizations)
            {
               o.OrganizationStatus = this.organizationService.GetStatus(User.Identity.Name, o.Id);
            }

            model.Organizations = organizations;

            return this.View(model);
        }

        [OutputCache(Duration = 360, VaryByParam = "none")]
        [ChildActionOnly]
        public ActionResult CachedEvents()
        {
            var events = this.eventService.GetUpcoming().Select(x=>this.mapper.Map<EventViewModel>(x)).ToList();
            
            return this.PartialView("_CachedEvents", events);
        }
    }
}