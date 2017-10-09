using System;
using System.Linq;
using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Data.Services.Contracts
{
    public interface IOrganizationService
    {
        void Create(Organization place);
        Organization GetById(Guid id);
        IQueryable<Organization> GetPlacesByNameOrUsername(string searchTerm);
        void Update(Organization place);
        Organization GetByUsername(string username);
        OrganizationStatus GetStatus(string username, Guid id);
        void CreateEvent(Event eventModel, Guid creatorId);
    }
}