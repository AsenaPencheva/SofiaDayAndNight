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
    public class OrganizationService : IOrganizationService
    {
        private readonly IEfDbSetWrapper<Organization> organizationSetWrapper;
        private readonly IUnitOfWork dbContext;
        private readonly IIndividualService individualService;

        public OrganizationService(IIndividualService individualService, IEfDbSetWrapper<Organization> organizationSetWrapper, IUnitOfWork dbContext)
        {
            Guard.WhenArgument(organizationSetWrapper, "organizationSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.organizationSetWrapper = organizationSetWrapper;
            this.dbContext = dbContext;
            this.individualService = individualService;
        }

        public Organization GetById(Guid? id)
        {
            Organization organization = null;
            if (id.HasValue)
            {
                organization = this.organizationSetWrapper.GetById(id.Value);
            }

            return organization;
        }

        //public void Create(Organization place)
        //{
        //    this.organizationSetWrapper.Add(place);
        //    this.dbContext.Commit();
        //}

        public IEnumerable<Organization> GetPlacesByNameOrUsername(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new List<Organization>();
            }
            return this.organizationSetWrapper.All.Where(i =>
                (string.IsNullOrEmpty(i.Name) ? false : i.Name.Contains(searchTerm))
                || (string.IsNullOrEmpty(i.User.UserName) ? false : i.User.UserName.Contains(searchTerm))).ToList();
        }

        public void Update(Organization organization)
        {
            if (organization != null)
            {
                this.organizationSetWrapper.Update(organization);
                dbContext.Commit();
            }
        }

        public Organization GetByUsername(string username)
        {
            Organization organization = null;
            if (!string.IsNullOrEmpty(username))
            {
                organization = this.organizationSetWrapper.All.FirstOrDefault(i => i.User.UserName == username);
            }

            return organization;
        }

        public Organization GetByUser(string userId)
        {
            Organization organization = null;
            if (!string.IsNullOrEmpty(userId))
            {
                organization = this.organizationSetWrapper.All.FirstOrDefault(i => i.User.Id == userId);
            }
            return organization;
        }

        public OrganizationStatus GetStatus(string currentUserId, Guid? id)
        {
            if (id.HasValue)
            {
                var organization = this.organizationSetWrapper.GetById(id.Value);
                if (organization != null)
                {
                    if (organization.User.Id == currentUserId)
                    {
                        return OrganizationStatus.isCurrent;
                    }
                    else if (organization.Followers.Where(x => x.User.Id == currentUserId).Count() > 0)
                    {
                        return OrganizationStatus.IsFollowed;
                    }
                }
            }

            return OrganizationStatus.None;
        }

        public void CreateEvent(Event eventModel, string creator)
        {
            if (!string.IsNullOrEmpty(creator) && eventModel != null)
            {
                var multimedia = new Multimedia();
                eventModel.Multimedia = multimedia;
                var organization = this.GetByUsername(creator);
                if (organization != null)
                {
                    organization.Events.Add(eventModel);
                    this.Update(organization);
                }
            }
        }

        public void Follow(string currentId, Guid? id)
        {
            if (!string.IsNullOrEmpty(currentId) && id.HasValue)
            {
                var current = this.individualService.GetByUser(currentId);
                var organization = this.GetById(id);

                if (current != null && organization != null)
                {
                    organization.Followers.Add(current);
                    this.Update(organization);
                }
            }
        }

        public void Unfollow(string currentId, Guid? id)
        {
            if (!string.IsNullOrEmpty(currentId) && id.HasValue)
            {
                var current = this.individualService.GetByUser(currentId);
                var organization = this.GetById(id);

                if (current != null && organization != null)
                {
                    organization.Followers.Remove(current);
                    this.Update(organization);
                }
            }
        }

        public IEnumerable<Event> GetPassedEvents(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var currentDate = DateTimeProvider.Current.UtcNow;

                return this.organizationSetWrapper.All
                    .Where(x => x.User.UserName == username).FirstOrDefault() != null ?
                    this.organizationSetWrapper.All
                    .Where(x => x.User.UserName == username).FirstOrDefault()
                    .Events.Where(e => e.Ends < currentDate).ToList() : new List<Event>();
            }

            return new List<Event>();
        }

        public IEnumerable<Event> GetCurrentEvents(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var currentDate = DateTimeProvider.Current.UtcNow;

                return this.organizationSetWrapper.All
                    .Where(x => x.User.UserName == username).FirstOrDefault() != null?
                    this.organizationSetWrapper.All
                    .Where(x => x.User.UserName == username).FirstOrDefault()
                    .Events.Where(e => e.Begins < currentDate && currentDate < e.Ends).ToList() : new List<Event>();
            }

            return new List<Event>();
        }

        public IEnumerable<Event> GetUpcomingEvents(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var currentDate = DateTimeProvider.Current.UtcNow;

                return this.organizationSetWrapper.All
                    .Where(x => x.User.UserName == username).FirstOrDefault() != null ?
                    this.organizationSetWrapper.All
                    .Where(x => x.User.UserName == username).FirstOrDefault()
                    .Events.Where(e => currentDate < e.Begins).ToList() : new List<Event>();
            }

            return new List<Event>();
        }

        public IEnumerable<Individual> GetFollowers(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var individual = this.GetByUsername(username);
                if (individual != null)
                {
                    return individual.Followers.ToList();
                }
            }

            return new List<Individual>();
        }

        public IEnumerable<Organization> GetAll()
        {
            return this.organizationSetWrapper.All.ToList();
        }
    }
}
