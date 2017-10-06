using System;
using System.Linq;

using SofiaDayAndNight.Common.Enums;
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
        Individual GetByUser(string userId);
        IQueryable<Individual> GetIndividualsByNameOrUsername(string searchTerm);
        void Update(Individual individual);
        IndividualStatus GetStatus(string currentUsername, Guid id);
        void SendFriendRequest(string currentUsername, Guid requestedId);
        Individual GetByUsername(string usename);
    }
}