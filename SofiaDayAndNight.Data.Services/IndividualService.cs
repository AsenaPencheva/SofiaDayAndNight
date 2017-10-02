using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

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

        public Individual GetById(Guid id)
        {
            var individual = this.individualSetWrapper.GetById(id);
            return individual;
        }

        public void Create(Individual individual)
        {
            this.individualSetWrapper.Add(individual);
            this.dbContext.Commit();
        }

        public IQueryable<Individual> GetIndividualsByNameOrUsername(string searchTerm)
        {
            var fullName = string.Empty;
            return string.IsNullOrEmpty(searchTerm) ? this.individualSetWrapper.All
                : this.individualSetWrapper.All.Where(i =>
                (string.IsNullOrEmpty(this.GetFullName(i)) ? false : this.GetFullName(i).Contains(searchTerm))
                || (string.IsNullOrEmpty(i.User.UserName) ? false : i.User.UserName.Contains(searchTerm)));
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

        public void CreateFriendship(Guid currentIndividualId, Guid friendToAddId)
        {
            var current = this.GetById(currentIndividualId);
            var friendToAdd = this.GetById(friendToAddId);

            current.Friends.Add(friendToAdd);
            friendToAdd.Friends.Add(current);

            this.Update(current);
            this.Update(friendToAdd);
        }

        //public void SendFriendRequest

        private string GetFullName(Individual individual)
        {
            return individual.FirstName + " " + individual.LastName;
        }

        public void Update(Individual individual)
        {
            this.individualSetWrapper.Update(individual);
            this.dbContext.Commit();
        }
    }
}
