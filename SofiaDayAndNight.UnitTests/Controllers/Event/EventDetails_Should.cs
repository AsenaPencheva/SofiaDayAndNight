using System;
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
using System.Web.Mvc;

namespace SofiaDayAndNight.UnitTests.Controllers.Event
{
    [TestFixture]
    public class EventDetails_Should
    {
        [Test]
        public void ReturnNotFoundRequest_WhenIdNotMatch()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            mockedEventService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((SofiaDayAndNight.Data.Models.Event)null);
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
                .WithCallTo(x => x.EventDetails(Guid.NewGuid()))
                .ShouldGiveHttpStatus(404);
        }

        [Test]
        public void ReturnViewWithCorrectModel_WhenIdMatch()
        {
            // Arrange
            var eventModel = new SofiaDayAndNight.Data.Models.Event();
            var viewModel = new EventViewModel();
            var title= "testTitle";
            viewModel.Title = title;

            var mockedEventService = new Mock<IEventService>();
            mockedEventService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(eventModel);
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<EventViewModel>(eventModel)).Returns(viewModel);

            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);

            // Act & Assert
            eventController
                .WithCallTo(x => x.EventDetails(Guid.NewGuid()))
                .ShouldRenderDefaultView()
                .WithModel<EventViewModel>(model =>
                {
                    Assert.AreEqual(model.Title, title);
                    Assert.AreEqual(Privacy.Private, model.Privacy);
                });
        }
    }
}
