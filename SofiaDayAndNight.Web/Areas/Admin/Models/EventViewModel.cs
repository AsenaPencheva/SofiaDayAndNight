using AutoMapper;
using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Web.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Web.Areas.Admin.Models
{
    public class EventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        [Editable(false)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Editable(false)]
        public Guid? IndividualId { get; set; }

        [Editable(false)]
        public Guid? OrganizationId { get; set; }

        [Editable(false)]
        public string CreatorUserName { get; set; }

        [UIHint("PrivacyEditor")]
        public Privacy Privacy { get; set; }

        [DisplayFormat(DataFormatString = "0:MM/dd/yy H:mm:ss")]
        public DateTime Begins { get; set; }

        [DisplayFormat(DataFormatString = "0:MM/dd/yy H:mm:ss")]
        public DateTime Ends { get; set; }

        [UIHint("EventTypeEditor")]
        public EventType EventType { get; set; }

        [UIHint("AccessTypeEditor")]
        public AccessType AccessType { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                .ForMember(viewModel => viewModel.CreatorUserName, cfg => cfg.MapFrom(model =>
                model.Individual != null ? model.Individual.User.UserName : model.Organization.User.UserName))
                .ForMember(viewModel => viewModel.IndividualId, cfg => cfg.MapFrom(model => model.Individual.Id))
                .ForMember(viewModel => viewModel.OrganizationId, cfg => cfg.MapFrom(model => model.Organization.Id));

        }
    }
}