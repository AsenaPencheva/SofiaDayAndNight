using System.Collections.Generic;
using System.Linq;
using System.Net;

using AutoMapper;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Controllers;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;

namespace SofiaDayAndNight.UnitTests.Controllers.Organization
{
    [TestFixture]
    class FollowersList_Should
    {
        [Test]
        public void ReturnBadRequest_WhenUsernameIsNull()
        {
            // Arrange
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new OrganizationController(
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);

            //var username = "testUsername";

            // Act & Assert
            controller
                .WithCallTo(x => x.FollowersList(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void CallMapMethod_WhenCollectionFound()
        {
            // Arrange
            var username = "testUsername";
            var model = new IndividualViewModel();

            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService.Setup(x => x.GetFollowers(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Individual>() { new SofiaDayAndNight.Data.Models.Individual() });
           
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<IndividualViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Individual>()))
                .Returns(model);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            //var principal = new Mock<IPrincipal>();
            //var controllerContext = new Mock<ControllerContext>();
            //principal.SetupGet(x => x.Identity.Name).Returns(username);
            //controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new OrganizationController(
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);
            //controller.ControllerContext = controllerContext.Object;

            // Act
            controller.FollowersList(username);

            // Assert
            mockedMapper.Verify(x => x.Map<IndividualViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Individual>()), Times.Once);
        }

        [Test]
        public void ReturnViewWithCorrectModel_WhenUsernameMatch()
        {
            //Arrange
            var username = "testUsername";
            var individual = new IndividualViewModel();
            
            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService.Setup(x => x.GetFollowers(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Individual>() { new SofiaDayAndNight.Data.Models.Individual() });
           
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<IndividualViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Individual>()))
                .Returns(individual);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new OrganizationController(
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);

            //Act & Assert
            controller
             .WithCallTo(x => x.FollowersList(username))
             .ShouldRenderPartialView("_IndividualsListPartial")
             .WithModel<IEnumerable<IndividualViewModel>>(model =>
             {
                 Assert.AreEqual(1, model.Count());
                 Assert.AreEqual(individual, model.First());
             });
        }
    }
}
