using System;
using System.Collections.Generic;

using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Data.Services.Contracts
{
    public interface IEventService
    {
        //void Ban(Guid id);
        //IQueryable<Event> GetAllCurrentEvents();
        //IQueryable<Event> GetAllPassedEvents();
        //IQueryable<Event> GetAllUpcomingEvents();
        Event GetById(Guid? id);
        IEnumerable<Event> GetEventsByName(string searchTerm);
        //void IndividualCreate(Event newEvent);
        //void OrganizationCreate(Event newEvent, Guid id);
        //void Unban(Guid id);
        void Update(Event eventToUpdate);
        IEnumerable<Event> GetAll();
        IEnumerable<Event> GetUpcoming();
    }
}