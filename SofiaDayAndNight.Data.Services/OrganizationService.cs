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
    public class OrganizationService : IOrganizationService
    {
        private readonly IEfDbSetWrapper<Organization> organizationSetWrapper;
        private readonly IUnitOfWork dbContext;

        public OrganizationService(IEfDbSetWrapper<Organization> organizationSetWrapper, IUnitOfWork dbContext)
        {
            Guard.WhenArgument(organizationSetWrapper, "organizationSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.organizationSetWrapper = organizationSetWrapper;
            this.dbContext = dbContext;
        }

        public Organization GetById(Guid id)
        {
            var place = this.organizationSetWrapper.GetById(id);
            return place;
        }

        public void Create(Organization place)
        {
            this.organizationSetWrapper.Add(place);
            this.dbContext.Commit();
        }

        public IQueryable<Organization> GetPlacesByNameOrUsername(string searchTerm)
        {
            var fullName = string.Empty;
            return string.IsNullOrEmpty(searchTerm) ? this.organizationSetWrapper.All
                : this.organizationSetWrapper.All.Where(i =>
                (string.IsNullOrEmpty(i.Name) ? false : i.Name.Contains(searchTerm))
                || (string.IsNullOrEmpty(i.User.UserName) ? false : i.User.UserName.Contains(searchTerm)));
        }

        public void Update(Organization place)
        {
            this.organizationSetWrapper.Update(place);
            dbContext.Commit();
        }

        public Organization GetByUsername(string username)
        {
            var organization = this.organizationSetWrapper.All.FirstOrDefault(i => i.User.UserName == username);
            return organization;
        }

        public OrganizationStatus GetStatus(string currentUsername, Guid id)
        {
            var organization = this.GetById(id);
            //if (organization.Followers.Where(x => x.User.UserName == currentUsername).Count() > 0)
            //{
            //    return OrganizationStatus.IsFollowed;
            //}          
                return OrganizationStatus.None;

        }
    }
}
