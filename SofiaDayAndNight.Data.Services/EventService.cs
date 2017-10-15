using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.Data.Services
{
    public class EventService : IEventService
    {
        private readonly IEfDbSetWrapper<Event> eventsSetWrapper;
        private readonly IIndividualService individualService;
        private readonly IOrganizationService organizationService;
        private readonly IUnitOfWork dbContext;

        public EventService(IEfDbSetWrapper<Event> eventsSetWrapper,
             IIndividualService individualService,
             IOrganizationService organizationService,
             IUnitOfWork dbContext)
        {
            Guard.WhenArgument(eventsSetWrapper, "EventsSetWrapper").IsNull().Throw();
            Guard.WhenArgument(individualService, "individualService").IsNull().Throw();
            Guard.WhenArgument(organizationService, "organizationService").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
            this.eventsSetWrapper = eventsSetWrapper;
            this.individualService = individualService;
            this.organizationService = organizationService;
        }

        public Event GetById(Guid? id)
        {
            Event currentEvent = null;
            if (id.HasValue)
            {
                currentEvent = this.eventsSetWrapper.GetById(id.Value);
            }

            return currentEvent;
        }
        
        public IEnumerable<Event> GetEventsByName(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new List<Event>();
            }

            return this.eventsSetWrapper.All.Where(e =>
                (string.IsNullOrEmpty(e.Title) ? false : e.Title.Contains(searchTerm))).ToList();
        }
  
        public void Update(Event eventToUpdate)
        {
            if (eventToUpdate != null)
            {
                this.eventsSetWrapper.Update(eventToUpdate);
                this.dbContext.Commit();
            }
        }

        public IEnumerable<Event> GetAll()
        {
            return this.eventsSetWrapper.All.ToList();
        }
    }
}