using System;
using System.Linq;

using SofiaDayAndNight.Data.Models;
using System.Collections.Generic;

namespace SofiaDayAndNight.Data.Services.Contracts
{
    public interface IEventService
    {
        void Ban(Guid id);
        IQueryable<Event> GetAllCurrentEvents();
        IQueryable<Event> GetAllPassedEvents();
        IQueryable<Event> GetAllUpcomingEvents();
        Event GetById(Guid id);
        IEnumerable<Event> GetEventsByName(string searchTerm);
        void IndividualCreate(Event newEvent);
        void OrganizationCreate(Event newEvent, Guid id);
        void Unban(Guid id);
        void Update(Event eventToUpdate);
    }
}