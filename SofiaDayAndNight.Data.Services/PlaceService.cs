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
        private readonly IEfDbSetWrapper<Place> placeSetWrapper;
        private readonly ISaveContext dbContext;

        public PlaceService(IEfDbSetWrapper<Place> placeSetWrapper, ISaveContext dbContext)
        {
            Guard.WhenArgument(placeSetWrapper, "placeSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.placeSetWrapper = placeSetWrapper;
            this.dbContext = dbContext;
        }

        public Place GetById(Guid id)
        {
            var place = this.placeSetWrapper.GetById(id);
            return place;
        }

        public void Create(Place place)
        {
            this.placeSetWrapper.Add(place);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<Place> GetPlacesByNameOrUsername(string searchTerm)
        {
            var fullName = string.Empty;
            return string.IsNullOrEmpty(searchTerm) ? this.placeSetWrapper.All.ToList()
                : this.placeSetWrapper.All.Where(i =>
                (string.IsNullOrEmpty(i.Name) ? false : i.Name.Contains(searchTerm))
                || (string.IsNullOrEmpty(i.User.UserName) ? false : i.User.UserName.Contains(searchTerm))).ToList();
        }

        public void Update(Place place)
        {
            this.placeSetWrapper.Update(place);
            dbContext.SaveChanges();
        }
    }
}
