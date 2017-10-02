using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using AutoMapper;

using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Web.Areas.IndividualArea.Models;
using SofiaDayAndNight.Web.Infrastructure;
using SofiaDayAndNight.Web.Models;

namespace SofiaDayAndNight.Web.Areas.OrganizationArea.Models
{
    public class OrganizationViewModel : IMapFrom<Organization>
    {
        public OrganizationViewModel()
        {
            // this.Events = new HashSet<EventViewModel>();
            this.Followers = new HashSet<IndividualViewModel>();

            this.Id = Guid.NewGuid();
        }

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string City { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        //public int AgeRestriction { get; set; }

        public ImageViewModel ProfileImage { get; set; }

        //public IEnumerable<EventViewModel> Events { get; set; }

        public IEnumerable<IndividualViewModel> Followers { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Organization, OrganizationViewModel>()
                .ForMember(viewModel => viewModel.City, cfg => cfg.MapFrom(model => model.User.City))
                .ForMember(viewModel => viewModel.UserName, cfg => cfg.MapFrom(model => model.User.UserName));
        }
    }
}