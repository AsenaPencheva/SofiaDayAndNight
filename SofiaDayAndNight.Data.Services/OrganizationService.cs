using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.Data.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IEfDbSetWrapper<Organization> placeSetWrapper;
        private readonly IUnitOfWork dbContext;

        public OrganizationService(IEfDbSetWrapper<Organization> placeSetWrapper, IUnitOfWork dbContext)
        {
            Guard.WhenArgument(placeSetWrapper, "placeSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.placeSetWrapper = placeSetWrapper;
            this.dbContext = dbContext;
        }

        public Organization GetById(Guid id)
        {
            var place = this.placeSetWrapper.GetById(id);
            return place;
        }

        public void Create(Organization place)
        {
            this.placeSetWrapper.Add(place);
            this.dbContext.Commit();
        }

        public IQueryable<Organization> GetPlacesByNameOrUsername(string searchTerm)
        {
            var fullName = string.Empty;
            return string.IsNullOrEmpty(searchTerm) ? this.placeSetWrapper.All
                : this.placeSetWrapper.All.Where(i =>
                (string.IsNullOrEmpty(i.Name) ? false : i.Name.Contains(searchTerm))
                || (string.IsNullOrEmpty(i.User.UserName) ? false : i.User.UserName.Contains(searchTerm)));
        }

        public void Update(Organization place)
        {
            this.placeSetWrapper.Update(place);
            dbContext.Commit();
        }
    }
}
