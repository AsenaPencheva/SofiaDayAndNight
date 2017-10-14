using System;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Web.Models;
using System.ComponentModel.DataAnnotations;
using SofiaDayAndNight.Web.Infrastructure;
using SofiaDayAndNight.Data.Models;
using AutoMapper;
using System.Web.Mvc;

namespace SofiaDayAndNight.Web.Areas.User.Models
{
    public class EventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public EventViewModel()
        {
            this.Id = Guid.NewGuid();
            this.Begins = DateTime.Now;
            this.Ends = DateTime.Now;
        }

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid? IndividualId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid? OrganizationId { get; set; }

        public string CreatorUserName { get; set; }

        //public int AgeRestriction { get; set; } // if not = 0

        public Privacy Privacy { get; set; }

        [DisplayFormat(DataFormatString = "0:MM/dd/yy H:mm:ss")]
        public DateTime Begins { get; set; }

        [DisplayFormat(DataFormatString = "0:MM/dd/yy H:mm:ss")]
        public DateTime Ends { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public EventType EventType { get; set; }

        public ImageViewModel Cover { get; set; }

        public ImageViewModel Creator { get; set; }

        //public IEnumerable<CommentViewModel> Comments { get; set; }

        //public IEnumerable<UserPersonalInfo> AttendedBy { get; set; }

        //public IEnumerable<UserPersonalInfo> AttendingBy { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                .ForMember(viewModel => viewModel.CreatorUserName, cfg => cfg.MapFrom(model =>
                model.Individual != null ? model.Individual.User.UserName : model.Organization.User.UserName))
                 .ForMember(viewModel => viewModel.Creator, cfg => cfg.MapFrom(model =>
                model.Individual != null ? model.Individual.ProfileImage : model.Organization.ProfileImage))
                .ForMember(viewModel => viewModel.IndividualId, cfg => cfg.MapFrom(model => model.Individual.Id))
                .ForMember(viewModel => viewModel.OrganizationId, cfg => cfg.MapFrom(model => model.Organization.Id));

        }
    }
}