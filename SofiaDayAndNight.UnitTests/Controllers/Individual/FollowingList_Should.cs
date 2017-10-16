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

namespace SofiaDayAndNight.UnitTests.Controllers.Individual
{
    [TestFixture]
    class FollowingList_Should
    {
        [Test]
        public void ReturnBadRequest_WhenUsernameIsNull()
        {
            // Arrange
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);

            //var username = "testUsername";

            // Act & Assert
            controller
                .WithCallTo(x => x.FollowingList(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void CallMapMethod_WhenCollectionFound()
        {
            // Arrange
            var username = "testUsername";
            var model = new OrganizationViewModel();

            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetFollowingOrganization(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Organization>() { new SofiaDayAndNight.Data.Models.Organization() });

            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<OrganizationViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Organization>()))
                .Returns(model);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            //var principal = new Mock<IPrincipal>();
            //var controllerContext = new Mock<ControllerContext>();
            //principal.SetupGet(x => x.Identity.Name).Returns(username);
            //controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);
            //controller.ControllerContext = controllerContext.Object;

            // Act
            controller.FollowingList(username);

            // Assert
            mockedMapper.Verify(x => x.Map<OrganizationViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Organization>()), Times.Once);
        }

        [Test]
        public void ReturnViewWithCorrectModel_WhenUsernameMatch()
        {
            //Arrange
            var username = "testUsername";
            var individual = new OrganizationViewModel();

            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetFollowingOrganization(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Organization>() { new SofiaDayAndNight.Data.Models.Organization() });

            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<OrganizationViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Organization>()))
                .Returns(individual);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);

            //Act & Assert
            controller
             .WithCallTo(x => x.FollowingList(username))
             .ShouldRenderPartialView("_OrganizationsListPartial")
             .WithModel<IEnumerable<OrganizationViewModel>>(model =>
             {
                 Assert.AreEqual(1, model.Count());
                 Assert.AreEqual(individual, model.First());
             });
        }
    }
}
