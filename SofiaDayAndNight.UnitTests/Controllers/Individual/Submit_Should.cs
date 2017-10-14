using AutoMapper;
using Moq;
using NUnit.Framework;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Controllers;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Controllers;
using SofiaDayAndNight.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace SofiaDayAndNight.UnitTests.Controllers.Individual
{
    [TestFixture]
    public class Submit_Should
    {
        [Test]
        public void ReturnProfileFormWithEmpty_WhenModelStatusIsInvalid()
        {
            // Arrange            
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);
            controller.ModelState.AddModelError("test", "test");

            var model = new IndividualViewModel();

            var mockedUpload = new Mock<HttpPostedFileBase>();

            // Act & Assert
            controller
                .WithCallTo(x => x.Submit(model, mockedUpload.Object))
                .ShouldRenderView("ProfileForm");
            //.ShouldRedirectTo<AccountController>(typeof(AccountController).GetMethod("ProfileForm"))
            //.WithRouteValue("model");
        }

        [Test]
        public void CallMapMethod_WhenModelStatusIsValid()
        {
            // Arrange    
            var username = "testUser";
            var user = new SofiaDayAndNight.Data.Models.User();
            var viewModel = new IndividualViewModel();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedUserProvider.Setup(x => x.FindByName(It.IsAny<string>())).Returns(user);
            mockedUserProvider.Setup(x => x.Update(user)).Verifiable();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<SofiaDayAndNight.Data.Models.Individual>(It.IsAny<IndividualViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Individual());

            var mockedUpload = new Mock<HttpPostedFileBase>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
               mockedIndividualService.Object,
               mockedMapper.Object,
               mockedPhotoHelper.Object,
               mockedUserProvider.Object);
            controller.ModelState.Clear();
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.Submit(viewModel, mockedUpload.Object);

            // Assert
            mockedMapper.Verify(x => x.Map<SofiaDayAndNight.Data.Models.Individual>(It.IsAny<IndividualViewModel>()), Times.Once);
        }

        [Test]
        public void CallFindByUsernameMethod_WhenModelStatusIsValid()
        {
            // Arrange    
            var username = "testUser";
            var user = new SofiaDayAndNight.Data.Models.User();
            var viewModel = new IndividualViewModel();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedUserProvider.Setup(x => x.FindByName(It.IsAny<string>())).Returns(user);
            mockedUserProvider.Setup(x => x.Update(user)).Verifiable();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<SofiaDayAndNight.Data.Models.Individual>(It.IsAny<IndividualViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Individual());

            var mockedUpload = new Mock<HttpPostedFileBase>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
               mockedIndividualService.Object,
               mockedMapper.Object,
               mockedPhotoHelper.Object,
               mockedUserProvider.Object);
            controller.ModelState.Clear();
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.Submit(viewModel, mockedUpload.Object);

            // Assert
            mockedUserProvider.Verify(x => x.FindByName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallUpdateMethod_WhenModelStatusIsValid()
        {
            // Arrange    
            var username = "testUser";
            var user = new SofiaDayAndNight.Data.Models.User();
            var viewModel = new IndividualViewModel();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedUserProvider.Setup(x => x.FindByName(It.IsAny<string>())).Returns(user);
            mockedUserProvider.Setup(x => x.Update(user)).Verifiable();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<SofiaDayAndNight.Data.Models.Individual>(It.IsAny<IndividualViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Individual());

            var mockedUpload = new Mock<HttpPostedFileBase>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
               mockedIndividualService.Object,
               mockedMapper.Object,
               mockedPhotoHelper.Object,
               mockedUserProvider.Object);
            controller.ModelState.Clear();
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.Submit(viewModel, mockedUpload.Object);

            // Assert
            mockedUserProvider.Verify(x => x.Update(user), Times.Once);
        }

        [Test]
        public void RedirectToAction_WhenModelStatusIsValid()
        {
            // Arrange    
            var username = "testUser";
            var user = new SofiaDayAndNight.Data.Models.User();
            var viewModel = new IndividualViewModel();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedUserProvider.Setup(x => x.FindByName(It.IsAny<string>())).Returns(user);
            mockedUserProvider.Setup(x => x.Update(user)).Verifiable();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<SofiaDayAndNight.Data.Models.Individual>(It.IsAny<IndividualViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Individual());

            var mockedUpload = new Mock<HttpPostedFileBase>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
               mockedIndividualService.Object,
               mockedMapper.Object,
               mockedPhotoHelper.Object,
               mockedUserProvider.Object);
            controller.ModelState.Clear();
            controller.ControllerContext = controllerContext.Object;

            //Act & Assert
            controller
             .WithCallTo(x => x.Submit(viewModel, mockedUpload.Object))
             .ShouldRedirectTo(typeof(IndividualController).GetMethod("ProfileDetails"));
        }

        [Test]
        public void ReturnNotFoundRequest_WhenUserNotFound()
        {
            // Arrange    
            var username = "testUser";
            var user = new User();
            var viewModel = new IndividualViewModel();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedUserProvider.Setup(x => x.FindByName(It.IsAny<string>())).Returns((User)null);
            mockedUserProvider.Setup(x => x.Update(user)).Verifiable();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<SofiaDayAndNight.Data.Models.Individual>(It.IsAny<IndividualViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Individual());

            var mockedUpload = new Mock<HttpPostedFileBase>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
               mockedIndividualService.Object,
               mockedMapper.Object,
               mockedPhotoHelper.Object,
               mockedUserProvider.Object);
            controller.ModelState.Clear();
            controller.ControllerContext = controllerContext.Object;

            //Act & Assert
            controller
             .WithCallTo(x => x.Submit(viewModel, mockedUpload.Object))
             .ShouldGiveHttpStatus(404);
        }

        [Test]
        public void NotCallUploadImage_WhenModelUploadIsInValid()
        {
            // Arrange    
            var username = "testUser";
            var user = new SofiaDayAndNight.Data.Models.User();
            var viewModel = new IndividualViewModel();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedUserProvider.Setup(x => x.FindByName(It.IsAny<string>())).Returns(user);
            mockedUserProvider.Setup(x => x.Update(user)).Verifiable();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<SofiaDayAndNight.Data.Models.Individual>(It.IsAny<IndividualViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Individual());

            var mockedUpload = new Mock<HttpPostedFileBase>();
            mockedPhotoHelper.Setup(x => x.UploadImage(mockedUpload.Object)).Verifiable();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
               mockedIndividualService.Object,
               mockedMapper.Object,
               mockedPhotoHelper.Object,
               mockedUserProvider.Object);
            controller.ModelState.Clear();
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.Submit(viewModel, mockedUpload.Object);

            // Assert
            mockedPhotoHelper.Verify(x => x.UploadImage(mockedUpload.Object), Times.Never);
        }

        [Test]
        public void CallUploadImage_WhenModelUploadIsValid()
        {
            // Arrange    
            var username = "testUser";
            var user = new SofiaDayAndNight.Data.Models.User();
            var viewModel = new IndividualViewModel();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            mockedUserProvider.Setup(x => x.FindByName(It.IsAny<string>())).Returns(user);
            mockedUserProvider.Setup(x => x.Update(user)).Verifiable();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<SofiaDayAndNight.Data.Models.Individual>(It.IsAny<IndividualViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Individual());

            var mockedUpload = new Mock<HttpPostedFileBase>();
            mockedUpload.SetupGet(x => x.ContentLength).Returns(1);
            mockedPhotoHelper.Setup(x => x.UploadImage(mockedUpload.Object)).Verifiable();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
               mockedIndividualService.Object,
               mockedMapper.Object,
               mockedPhotoHelper.Object,
               mockedUserProvider.Object);
            controller.ModelState.Clear();
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.Submit(viewModel, mockedUpload.Object);

            // Assert
            mockedPhotoHelper.Verify(x => x.UploadImage(mockedUpload.Object), Times.Once);
        }
    }
}
