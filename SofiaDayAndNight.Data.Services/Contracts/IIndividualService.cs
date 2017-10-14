using System;
using System.Collections;
using System.Linq;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;
using System.Collections.Generic;

namespace SofiaDayAndNight.Data.Services.Contracts
{
    public interface IIndividualService
    {
        void AttendEvent(Guid? individualId, Event eventToAttend);
        //void Create(Individual individual);
        void FollowPlace(Guid? individualId, Organization placeToFollow);
        Individual GetByUsername(string usename);
        Individual GetById(Guid? id);
        Individual GetByUser(string userId);
        IEnumerable<Individual> GetIndividualsByNameOrUsername(string searchTerm);
        void Update(Individual individual);
        IndividualStatus GetStatus(string currentUserId, Guid? id);
        void SendFriendRequest(string currentUserId, Guid? id);
        void CancelFriendRequest(string currentUserId, Guid? id);
        void ConfirmFriendship(string currentUserId, Guid? id);
        void RemoveFriendship(string currentUserId, Guid? id);
        IEnumerable<Individual> GetAll();
        IEnumerable<Individual> GetFriendsRequests(string username);
        IEnumerable<Individual> GetFriends(string username);
        void CreateEvent(Event eventModel, string creator);
        //IEnumerable<Event> GetEvents(string username);
        IEnumerable<Event> GetUpcomingEvents(string username);
        IEnumerable<Event> GetPassedEvents(string username);
        IEnumerable<Event> GetCurrentEvents(string username);
        IEnumerable<Organization> GetFollowingOrganization(string username);
    }
}