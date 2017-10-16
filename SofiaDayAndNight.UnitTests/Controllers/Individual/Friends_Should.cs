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
    public class Friends_Should
    {
        [Test]
        public void ReturnBadRequest_WhenUsernameIsNull()
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

            //var username = "testUsername";

            // Act & Assert
            controller
                .WithCallTo(x => x.Friends(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void CallMapMethod_WhenCollectionFound()
        {
            // Arrange
            var username = "testUsername";
            var model = new IndividualViewModel();

            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetFriends(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Individual>() { new SofiaDayAndNight.Data.Models.Individual() });

            var mockedOrganizationService = new Mock<IOrganizationService>();
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

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);
            //controller.ControllerContext = controllerContext.Object;

            // Act
            controller.Friends(username);

            // Assert
            mockedMapper.Verify(x => x.Map<IndividualViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Individual>()), Times.Once);
        }

        [Test]
        public void ReturnViewWithCorrectCollection_WhenUsernameMatch()
        {
            //Arrange
            var username = "testUsername";
            var model = new IndividualViewModel();

            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetFriends(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Individual>() { new SofiaDayAndNight.Data.Models.Individual() });

            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<IndividualViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Individual>()))
                .Returns(model);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);

            //Act & Assert
            controller
             .WithCallTo(x => x.Friends(username))
             .ShouldRenderPartialView("_FriendsListPartial")
             .WithModel<IEnumerable<IndividualViewModel>>(c =>
             {
                 Assert.AreEqual(1, c.Count());
                 Assert.AreEqual(model.Id, c.First().Id);
             });
        }
    }
}
