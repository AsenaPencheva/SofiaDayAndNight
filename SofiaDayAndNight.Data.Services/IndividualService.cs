using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Common;

namespace SofiaDayAndNight.Data.Services
{
    public class IndividualService : IIndividualService
    {
        private readonly IEfDbSetWrapper<Individual> individualSetWrapper;
        private readonly IUnitOfWork dbContext;

        public IndividualService(IEfDbSetWrapper<Individual> individualSetWrapper,
            IUnitOfWork dbContext)
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
            Individual individual = null;
            if (!string.IsNullOrEmpty(userId))
            {
                individual = this.individualSetWrapper.All.FirstOrDefault(i => i.User.Id == userId);
            }

            return individual;
        }

        //public void Create(Individual individual)
        //{
        //    this.individualSetWrapper.Add(individual);
        //    this.dbContext.Commit();
        //}

        public IEnumerable<Individual> GetIndividualsByNameOrUsername(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new List<Individual>();
            }

            return this.individualSetWrapper.All.Where(i =>
                (string.IsNullOrEmpty(i.FirstName + i.LastName) ? false : (i.FirstName + " " + i.LastName).Contains(searchTerm))
                || (string.IsNullOrEmpty(i.User.UserName) ? false : i.User.UserName.Contains(searchTerm))).ToList();
        }

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

        public void AttendEvent(Guid? individualId, Event eventToAttend)
        {
            if (individualId.HasValue && eventToAttend != null)
            {
                var individual = this.GetById(individualId);
                if (individual != null)
                {
                    individual.EventsAttended.Add(eventToAttend);
                    this.Update(individual);
                }
            }
        }

        public void FollowPlace(Guid? individualId, Organization placeToFollow)
        {
            if (individualId.HasValue && placeToFollow != null)
            {
                var individual = this.GetById(individualId);
                if (individual != null)
                {
                    individual.Following.Add(placeToFollow);
                    this.Update(individual);
                }
            }
        }

        public void Update(Individual individual)
        {
            if (individual != null)
            {
                this.individualSetWrapper.Update(individual);
                this.dbContext.Commit();
            }          
        }

        public IndividualStatus GetStatus(string currentUsername, Guid? id)
        {
            // Guid?
            var current = this.GetByUsername(currentUsername);
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

        public void SendFriendRequest(string currentUser, Guid? id)
        {
            if (!string.IsNullOrEmpty(currentUser) && id.HasValue)
            {
                var current = this.GetByUsername(currentUser);
                var friendToAdd = this.GetById(id);
                if (current != null && friendToAdd != null)
                {
                    current.FriendRequested.Add(friendToAdd);
                    friendToAdd.FriendRequests.Add(current);

                    this.Update(current);
                    this.Update(friendToAdd);
                }
            }
        }

        public void CancelFriendRequest(string currentUser, Guid? id)
        {
            if (!string.IsNullOrEmpty(currentUser) && id.HasValue)
            {
                var current = this.GetByUsername(currentUser);
                var friendToRemove = this.GetById(id);
                if (current != null && friendToRemove != null)
                {
                    current.FriendRequested.Remove(friendToRemove);
                    friendToRemove.FriendRequests.Remove(current);

                    this.Update(current);
                    this.Update(friendToRemove);
                }
            }
        }

        public void ConfirmFriendship(string currentUser, Guid? id)
        {
            if (!string.IsNullOrEmpty(currentUser) && id.HasValue)
            {
                var current = this.GetByUsername(currentUser);
                var friendToAdd = this.GetById(id);
                if (current != null && friendToAdd != null)
                {
                    current.FriendRequests.Remove(friendToAdd);
                    friendToAdd.FriendRequested.Remove(current);
                    current.Friends.Add(friendToAdd);
                    friendToAdd.Friends.Add(current);

                    this.Update(current);
                    this.Update(friendToAdd);
                }
            }
        }

        public void RemoveFriendship(string currentUser, Guid? id)
        {
            if (!string.IsNullOrEmpty(currentUser) && id.HasValue)
            {
                var current = this.GetByUsername(currentUser);
                var friendToRemove = this.GetById(id);
                if (current != null && friendToRemove != null)
                {
                    current.Friends.Remove(friendToRemove);
                    friendToRemove.Friends.Remove(current);

                    this.Update(current);
                    this.Update(friendToRemove);
                }
            }
        }

        public void CreateEvent(Event eventModel, string creator)
        {
            if (!string.IsNullOrEmpty(creator) && eventModel != null)
            {
                var multimedia = new Multimedia();
                eventModel.Multimedia = multimedia;
                var individual = this.GetByUsername(creator);
                if (individual != null)
                {
                    individual.Events.Add(eventModel);
                    this.Update(individual);
                }
            }
        }

        public IEnumerable<Event> GetUpcomingEvents(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var currentDate = DateTimeProvider.Current.UtcNow;
                //var individual = this.GetByUsername(username);

                return this.individualSetWrapper.All
                   .FirstOrDefault(x => x.User.UserName == username) != null ?
                   this.individualSetWrapper.All
                   .FirstOrDefault(x => x.User.UserName == username)
                    .Events.Where(e => currentDate < e.Begins).ToList() : new List<Event>();
            }

            return new List<Event>();
        }

        public IEnumerable<Event> GetCurrentEvents(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var currentDate = DateTimeProvider.Current.UtcNow;

                return this.individualSetWrapper.All
                 .Where(x => x.User.UserName == username).FirstOrDefault() != null ?
                  this.individualSetWrapper.All
                 .Where(x => x.User.UserName == username).FirstOrDefault()
                 .Events.Where(e => e.Begins < currentDate && currentDate < e.Ends).ToList() : new List<Event>();
            }

            return new List<Event>();
        }

        public IEnumerable<Event> GetPassedEvents(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var currentDate = DateTimeProvider.Current.UtcNow;

                return this.individualSetWrapper.All
               .Where(x => x.User.UserName == username).FirstOrDefault()!=null?
                    this.individualSetWrapper.All
               .Where(x => x.User.UserName == username).FirstOrDefault()
               .Events.Where(e => e.Ends < currentDate).ToList():new List<Event>();
            }

            return new List<Event>();
        }

        public IEnumerable<Organization> GetFollowingOrganization(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var individual = this.GetByUsername(username);
                if (individual != null)
                {
                    return individual.Following.ToList();
                }
            }
            return new List<Organization>();
        }

        public IEnumerable<Individual> GetAll()
        {
            return this.individualSetWrapper.All.ToList();
        }
    }
}
