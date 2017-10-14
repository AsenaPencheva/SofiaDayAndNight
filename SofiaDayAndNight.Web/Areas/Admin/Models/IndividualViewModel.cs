using AutoMapper;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Web.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Web.Areas.Admin.Models
{
    public class IndividualViewModel : IMapFrom<Individual>, IHaveCustomMappings
    {
        [Editable(false)]
        public Guid Id { get; set; }

        [Editable(false)]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public int Age { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Individual, IndividualViewModel>()
                .ForMember(viewModel => viewModel.City, cfg => cfg.MapFrom(model => model.User.City))
                .ForMember(viewModel => viewModel.UserName, cfg => cfg.MapFrom(model => model.User.UserName));
        }
    }
}