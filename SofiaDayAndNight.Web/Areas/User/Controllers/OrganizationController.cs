using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Areas.User.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService organizationService;
        private readonly IMapper mapper;

        public OrganizationController(IOrganizationService organizationService, IMapper mapper)
        {
            this.organizationService = organizationService;
            this.mapper = mapper;
        }

        // GET: Organization/Organization
        public ActionResult Index(OrganizationViewModel model)
        {
            ViewBag.Name = model.Name;
            return View();
        }


        [HttpPost]
        public ActionResult Submit(OrganizationViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                return View("ProfileForm", model);
            }

            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var organization = this.mapper.Map<Organization>(model);
            organization.CreatedOn = DateTime.Now;
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
                organization.ProfileImage = image;
            }

            organization.User = user;
            
            user.Organization = organization;
            user.IsCompleted = true;
            System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Update(user);

            return this.RedirectToAction("Index");
        }
    }
}