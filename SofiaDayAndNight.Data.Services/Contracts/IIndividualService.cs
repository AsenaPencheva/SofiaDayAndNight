using System;
using System.Linq;

using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Data.Services.Contracts
{
    public interface IIndividualService
    {
        void AttendEvent(Guid individualId, Event eventToAttend);
        void Create(Individual individual);
        void CreateFriendship(Guid currentIndividualId, Guid friendToAddId);
        void FollowPlace(Guid individualId, Organization placeToFollow);
        Individual GetById(Guid id);
        IQueryable<Individual> GetIndividualsByNameOrUsername(string searchTerm);
        void Update(Individual individual);
    }
}