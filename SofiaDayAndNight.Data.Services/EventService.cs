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
        private readonly IEfDbSetWrapper<Individual> individualSetWrapper;
        private readonly IEfDbSetWrapper<Organization> organizationSetWrapper;
        private readonly IUnitOfWork dbContext;

        public EventService(IEfDbSetWrapper<Event> eventsSetWrapper,
            IEfDbSetWrapper<Individual> individualSetWrapper,
            IEfDbSetWrapper<Organization> organizationSetWrapper, 
            IUnitOfWork dbContext)
        {
            Guard.WhenArgument(eventsSetWrapper, "EventsSetWrapper").IsNull().Throw();
            Guard.WhenArgument(individualSetWrapper, "individualSetWrapper").IsNull().Throw();
            Guard.WhenArgument(organizationSetWrapper, "organizationSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
            this.eventsSetWrapper = eventsSetWrapper;
            this.individualSetWrapper = individualSetWrapper;
            this.organizationSetWrapper = organizationSetWrapper;
        }

        public Event GetById(Guid id)
        {
            var currentEvent = this.eventsSetWrapper.GetById(id);
            return currentEvent;
        }

        public void OrganizationCreate(Event newEvent, Guid id)
        {
           var organization= this.organizationSetWrapper.GetById(id);
            organization.Events.Add(newEvent);
            this.eventsSetWrapper.Add(newEvent);
            dbContext.Commit();
        }

        public void IndividualCreate(Event newEvent)
        {
            this.eventsSetWrapper.Add(newEvent);
            dbContext.Commit();
        }

        public IEnumerable<Event> GetEventsByName(string searchTerm)
        {
            return this.eventsSetWrapper.All.Where(e =>
                (string.IsNullOrEmpty(e.Title) ? false : e.Title.Contains(searchTerm))).ToList();
        }

        public IQueryable<Event> GetAllUpcomingEvents()
        {
            var currentDate = DateTime.Now;
            return this.eventsSetWrapper.All.Where(e => currentDate < e.Begins);
        }

        public IQueryable<Event> GetAllCurrentEvents()
        {
            var currentDate = DateTime.Now;
            return this.eventsSetWrapper.All.Where(e => e.Begins < currentDate && currentDate < e.Ends);
        }

        public IQueryable<Event> GetAllPassedEvents()
        {
            var currentDate = DateTime.Now;
            return this.eventsSetWrapper.All.Where(e => e.Ends < currentDate);
        }

        public void Ban(Guid id)
        {
            var eventToUpdate = this.GetById(id);
            eventToUpdate.IsForbidden = true;
            this.Update(eventToUpdate);
        }

        public void Unban(Guid id)
        {
            var eventToUpdate = this.GetById(id);
            eventToUpdate.IsForbidden = false;
            this.Update(eventToUpdate);
        }

        public void Update(Event eventToUpdate)
        {
            this.eventsSetWrapper.Update(eventToUpdate);
            this.dbContext.Commit();
        }
    }
}