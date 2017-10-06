using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult ProfileDetails(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var individual = this.individualService.GetByUsername(username);
            if (individual == null)
            {
                return HttpNotFound();
            }

            var model = this.mapper.Map<IndividualViewModel>(individual);

            model.IndividualStatus = this.individualService.GetStatus(this.User.Identity.Name, model.Id);

            return this.View(model);
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

            return RedirectToAction("ProfileDetails", new { area = "User", username = user.UserName });
        }

        [HttpPost]
        public ActionResult SendFriendRequest(Guid id)
        {
            this.individualService.SendFriendRequest(this.User.Identity.Name, id);

            return this.RedirectToAction("ProfileDetails", new { username = this.User.Identity.Name });
        }

        //[HttpPost]
        //public ActionResult CancelFriendRequest(Guid id)
        //{
        //    this.individualService.SendFriendRequest(this.User.Identity.Name, id);

        //    return this.RedirectToAction("ProfileDetails", new { id = id });
        //}

        //[HttpPost]
        //public ActionResult CancelFriendship(Guid id)
        //{
        //    this.individualService.SendFriendRequest(this.User.Identity.Name, id);

        //    return this.RedirectToAction("ProfileDetails", new { id = id });
        //}
    }
}