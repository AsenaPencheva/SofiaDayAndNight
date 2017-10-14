using System.Net;

using AutoMapper;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Controllers;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;

namespace SofiaDayAndNight.UnitTests.Controllers.Event
{
    [TestFixture]
    public class CreateGet_Should
    {
        [Test]
        public void ReturnViewWithEmptyModelAndCorrectUsername_WhenCorrectUsernameIsPassed()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);

            var username = "testUsername";

            // Act & Assert
            eventController
                .WithCallTo(x => x.Create(username))
                .ShouldRenderDefaultView()
                .WithModel<EventViewModel>(model =>
                {
                    Assert.AreEqual(username, model.CreatorUserName);
                    Assert.IsNull(model.Title);
                    Assert.AreEqual(Privacy.Private, model.Privacy);
                    Assert.IsNull(model.Description);
                });
        }

        [Test]
        public void ReturnBadRequest_WhenUsernameINull()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);

            //var username = "testUsername";

            // Act & Assert
            eventController
                .WithCallTo(x => x.Create(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }
    }
}
