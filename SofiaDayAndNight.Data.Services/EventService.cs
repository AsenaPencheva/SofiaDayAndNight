using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Data.Services
{
    public class EventService
    {
        private readonly IEfDbSetWrapper<Event> eventsSetWrapper;

        private readonly ISaveContext dbContext;

        public EventService(IEfDbSetWrapper<Event> eventsSetWrapper, ISaveContext dbContext)
        {
            Guard.WhenArgument(eventsSetWrapper, "EventsSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
            this.eventsSetWrapper = eventsSetWrapper;
        }

        public Event GetById(Guid id)
        {
            var currentEvent = this.eventsSetWrapper.GetById(id);
            return currentEvent;
        }

        public void Create(Event newEvent)
        {
            this.eventsSetWrapper.Add(newEvent);
            dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetEventsByNameOrPlace(string searchTerm)
        {
            return string.IsNullOrEmpty(searchTerm) ? this.eventsSetWrapper.All.ToList()
                : this.eventsSetWrapper.All.Where(e =>
                (string.IsNullOrEmpty(e.Title) ? false : e.Title.Contains(searchTerm))
                ||
                (string.IsNullOrEmpty(e.Place.Name) ? false : e.Place.Name.Contains(searchTerm))).ToList();
        }

        public IEnumerable<Event> GetAllUpcomingEvents()
        {
            var currentDate = DateTime.Now;
            return this.eventsSetWrapper.All.Where(e => currentDate < e.Begins).ToList();
        }

        public IEnumerable<Event> GetAllCurrentEvents()
        {
            var currentDate = DateTime.Now;
            return this.eventsSetWrapper.All.Where(e => e.Begins < currentDate && currentDate < e.Ends).ToList();
        }

        public IEnumerable<Event> GetAllPassedEvents()
        {
            var currentDate = DateTime.Now;
            return this.eventsSetWrapper.All.Where(e => e.Ends < currentDate).ToList();
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
            this.dbContext.SaveChanges();
        }
    }
}