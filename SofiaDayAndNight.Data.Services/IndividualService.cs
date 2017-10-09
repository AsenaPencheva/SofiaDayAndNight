using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.Data.Services
{
    public class IndividualService : IIndividualService
    {
        private readonly IEfDbSetWrapper<Individual> individualSetWrapper;
        private readonly IUnitOfWork dbContext;

        public IndividualService(IEfDbSetWrapper<Individual> individualSetWrapper, IUnitOfWork dbContext)
        {
            Guard.WhenArgument(individualSetWrapper, "individualSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.individualSetWrapper = individualSetWrapper;
            this.dbContext = dbContext;
        }

        public Individual GetById(Guid? id)
        {
            Individual individual = null;
            if (id.HasValue)
            {
                individual = this.individualSetWrapper.GetById(id.Value);
            }

            return individual;
        }

        public Individual GetByUsername(string usename)
        {
            Individual individual = null;
            if (!string.IsNullOrEmpty(usename))
            {
                individual = this.individualSetWrapper.All.FirstOrDefault(i => i.User.UserName == usename);
            }
            return individual;
        }

        public Individual GetByUser(string userId)
        {
            // if userId is null!!!
            var individual = this.individualSetWrapper.All.FirstOrDefault(i => i.User.Id == userId);
            return individual;
        }

        //public void Create(Individual individual)
        //{
        //    this.individualSetWrapper.Add(individual);
        //    this.dbContext.Commit();
        //}

        //public IEnumerable<Individual> GetIndividualsByNameOrUsername(string searchTerm)
        //{
        //    var fullName = string.Empty;
        //    return string.IsNullOrEmpty(searchTerm) ? this.individualSetWrapper.All
        //        : this.individualSetWrapper.All.Where(i =>
        //        (string.IsNullOrEmpty(this.GetFullName(i)) ? false : this.GetFullName(i).Contains(searchTerm))
        //        || (string.IsNullOrEmpty(i.User.UserName) ? false : i.User.UserName.Contains(searchTerm)));
        //}

        public IEnumerable<Individual> GetFriendsRequests(string username)
        {
            var result = new List<Individual>();
            if (!string.IsNullOrEmpty(username))
            {
                var individual = this.GetByUsername(username);
                if (individual != null)
                {
                    result = individual.FriendRequests.ToList();
                }
            }

            return result;
        }

        public IEnumerable<Individual> GetFriends(string username)
        {
            var result = new List<Individual>();
            if (!string.IsNullOrEmpty(username))
            {
                var individual = this.GetByUsername(username);
                if (individual != null)
                {
                    result = individual.Friends.ToList();
                }
            }

            return result;
        }

        public void AttendEvent(Guid individualId, Event eventToAttend)
        {
            var individual = this.GetById(individualId);
            individual.EventsAttended.Add(eventToAttend);
            this.Update(individual);
        }

        public void FollowPlace(Guid individualId, Organization placeToFollow)
        {
            var individual = this.GetById(individualId);
            individual.Following.Add(placeToFollow);
            this.Update(individual);
        }

        private string GetFullName(Individual individual)
        {
            return individual.FirstName + " " + individual.LastName;
        }

        public void Update(Individual individual)
        {
            this.individualSetWrapper.Update(individual);
            this.dbContext.Commit();
        }

        public IndividualStatus GetStatus(string currentUserId, Guid id)
        {
            // Guid?
            var current = this.GetByUser(currentUserId);
            if (current != null)
            {
                if (current.Id == id)
                {
                    return IndividualStatus.IsCurrent;
                }
                else if (current.Friends.Where(x => x.Id == id).Count() > 0)
                {
                    return IndividualStatus.IsFriend;
                }
                else if (current.FriendRequested.Where(x => x.Id == id).Count() > 0)
                {
                    return IndividualStatus.IsRequested;
                }
                else if (current.FriendRequests.Where(x => x.Id == id).Count() > 0)
                {
                    return IndividualStatus.HasRequest;
                }
            }

            return IndividualStatus.None;

        }

        public void SendFriendRequest(string currentUser, string usename)
        {
            var current = this.GetByUsername(currentUser);
            var friendToAdd = this.GetByUsername(usename);

            current.FriendRequested.Add(friendToAdd);
            friendToAdd.FriendRequests.Add(current);

            this.Update(current);
            this.Update(friendToAdd);
        }


        public void CancelFriendRequest(string currentUsername, string username)
        {
            var current = this.GetByUsername(currentUsername);
            var friendToRemove = this.GetByUsername(username);

            current.FriendRequested.Remove(friendToRemove);
            friendToRemove.FriendRequests.Remove(current);

            this.Update(current);
            this.Update(friendToRemove);
        }

        public void ConfirmFriendship(string currentUsername, string username)
        {
            var current = this.GetByUsername(currentUsername);
            var friendToAdd = this.GetByUsername(username);

            current.FriendRequests.Remove(friendToAdd);
            friendToAdd.FriendRequested.Remove(current);
            current.Friends.Add(friendToAdd);
            friendToAdd.Friends.Add(current);

            this.Update(current);
            this.Update(friendToAdd);
        }

        public void RemoveFriendship(string currentUsername, string username)
        {
            var current = this.GetByUsername(currentUsername);
            var friendToRemove = this.GetByUsername(username);

            current.Friends.Remove(friendToRemove);
            friendToRemove.Friends.Remove(current);

            this.Update(current);
            this.Update(friendToRemove);
        }

        public void CreateEvent(Event eventModel, string creator)
        {
            var multimedia = new Multimedia();
            eventModel.Multimedia = multimedia;
            var individual = this.GetByUsername(creator);
            individual.Events.Add(eventModel);
            this.Update(individual);
        }

        public IEnumerable<Event> GetEvents(string username)
        {
            var individual = this.GetByUsername(username);

            return individual.Events.ToList();
        }

        public IEnumerable<Event> GetUpcomingEvents(string username)
        {
            var currentDate = DateTime.Now;
            //var individual = this.GetByUsername(username);

            return this.individualSetWrapper.All
                .Where(x => x.User.UserName == username).FirstOrDefault()
                .Events.Where(e => currentDate < e.Begins).ToList();

            //return individual.Events.Where(e => currentDate < e.Begins).ToList();
        }

        public IEnumerable<Event> GetCurrentEvents(string username)
        {
            var currentDate = DateTime.Now;
            var individual = this.GetByUsername(username);


            return individual.Events.Where(e => e.Begins < currentDate && currentDate < e.Ends).ToList();
        }

        public IEnumerable<Event> GetPassedEvents(string username)
        {
            var currentDate = DateTime.Now;
            var individual = this.GetByUsername(username);

            return this.individualSetWrapper.All
               .Where(x => x.User.UserName == username).FirstOrDefault()
               .Events.Where(e => e.Ends < currentDate).ToList();

            //return individual.Events.Where(e => e.Ends < currentDate).ToList();
        }
    }
}
