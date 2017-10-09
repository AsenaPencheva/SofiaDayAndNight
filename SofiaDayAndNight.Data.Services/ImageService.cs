using System;

using Bytes2you.Validation;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.Data.Services
{
    public class ImageService : IImageService
    {
        private readonly IEfDbSetWrapper<Image> imageSetWrapper;
        private readonly IUnitOfWork dbContext;

        public ImageService(IEfDbSetWrapper<Image> imageSetWrapper, IUnitOfWork dbContext)
        {
            Guard.WhenArgument(imageSetWrapper, "imageSetWrapper").IsNull().Throw();
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.imageSetWrapper = imageSetWrapper;
            this.dbContext = dbContext;
        }

        public void Create(Image image)
        {
            this.imageSetWrapper.Add(image);
            this.dbContext.Commit();
        }

        public Image GetById(Guid id)
        {
            var image = this.imageSetWrapper.GetById(id);
            return image;
        }
    }
}
