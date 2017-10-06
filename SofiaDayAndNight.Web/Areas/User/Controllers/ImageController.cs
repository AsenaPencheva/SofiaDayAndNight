using AutoMapper;
using SofiaDayAndNight.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Areas.User.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public ImageController(IImageService imageService, IMapper mapper)
        {
            this.imageService = imageService;
            this.mapper = mapper;
        }
        // GET: User/Image
        public ActionResult Index(Guid id)
        {
            var imageToRetrieve = imageService.GetById(id);
            return File(imageToRetrieve.Data, imageToRetrieve.ContentType);
        }
    }
}