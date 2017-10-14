using AutoMapper;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SofiaDayAndNight.Web.Areas.Admin.Models
{
    public class OrganizationViewModel : IMapFrom<Organization>, IHaveCustomMappings
    {
        [Editable(false)]
        public Guid Id { get; set; }

        [Editable(false)]
        public string UserName { get; set; }

        public string City { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Organization, OrganizationViewModel>()
                .ForMember(viewModel => viewModel.City, cfg => cfg.MapFrom(model => model.User.City))
                .ForMember(viewModel => viewModel.UserName, cfg => cfg.MapFrom(model => model.User.UserName));
        }
    }
}