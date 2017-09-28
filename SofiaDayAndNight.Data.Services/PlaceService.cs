using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Data.Services
{
    public class PlaceService
    {
        private readonly IEfDbSetWrapper<Organization> placeSetWrapper;
        private readonly IUnitOfWork dbContext;

        public PlaceService(IEfDbSetWrapper<Organization> placeSetWrapper, IUnitOfWork dbContext)
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

        public IEnumerable<Organization> GetPlacesByNameOrUsername(string searchTerm)
        {
            var fullName = string.Empty;
            return string.IsNullOrEmpty(searchTerm) ? this.placeSetWrapper.All.ToList()
                : this.placeSetWrapper.All.Where(i =>
                (string.IsNullOrEmpty(i.Name) ? false : i.Name.Contains(searchTerm))
                || (string.IsNullOrEmpty(i.User.UserName) ? false : i.User.UserName.Contains(searchTerm))).ToList();
        }

        public void Update(Organization place)
        {
            this.placeSetWrapper.Update(place);
            dbContext.Commit();
        }
    }
}
