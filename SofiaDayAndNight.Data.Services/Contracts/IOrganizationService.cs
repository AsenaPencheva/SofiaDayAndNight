using System;
using System.Collections.Generic;
using System.Linq;
using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Data.Services.Contracts
{
    public interface IOrganizationService
    {
        //void Create(Organization place);
        Organization GetById(Guid? id);
        IEnumerable<Organization> GetPlacesByNameOrUsername(string searchTerm);
        void Update(Organization place);
        Organization GetByUsername(string username);
        OrganizationStatus GetStatus(string userId, Guid? id);
        void CreateEvent(Event eventModel, string username);
        void Follow(string currentId, Guid? id);
        void Unfollow(string currentId, Guid? id);
        IEnumerable<Event> GetPassedEvents(string username);
        IEnumerable<Event> GetCurrentEvents(string username);
        IEnumerable<Event> GetUpcomingEvents(string username);
        IEnumerable<Individual> GetFollowers(string username);
        IEnumerable<Organization> GetAll();
    }
}