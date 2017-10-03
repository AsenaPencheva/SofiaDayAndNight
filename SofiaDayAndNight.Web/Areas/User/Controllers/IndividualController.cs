using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Areas.User.Controllers
{
    [Authorize]
    public class IndividualController : Controller
    {
        private readonly IIndividualService individualService;
        private readonly IMapper mapper;

        public IndividualController(IIndividualService individualService, IMapper mapper)
        {
            this.individualService = individualService;
            this.mapper = mapper;
        }

        public ActionResult Index(IndividualViewModel model)
        {
            ViewBag.Name = model.FirstName;
            return View();
        }

        [HttpPost]
        public ActionResult Submit(IndividualViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                return View("ProfileForm", model);
            }

            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var individual = this.mapper.Map<Individual>(model);
            individual.CreatedOn = DateTime.Now;
            if (upload != null && upload.ContentLength > 0)
            {
                var image = new Image
                {
                    Name = System.IO.Path.GetFileName(upload.FileName),
                    ContentType = upload.ContentType
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    image.Data = reader.ReadBytes(upload.ContentLength);
                }
                individual.ProfileImage = image;
            }

            individual.User = user;

            //this.individualService.Create(individual);
            user.Individual = individual;
            user.IsCompleted = true;
            System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Update(user);

            model= this.mapper.Map<IndividualViewModel>(individual);
            return this.RedirectToAction("Index", model);
        }
    }
}