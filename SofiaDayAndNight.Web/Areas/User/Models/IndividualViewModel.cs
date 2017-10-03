using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using AutoMapper;

using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Web.Infrastructure;
using SofiaDayAndNight.Web.Models;

namespace SofiaDayAndNight.Web.Areas.User.Models
{
    public class IndividualViewModel : IMapFrom<Individual>, IHaveCustomMappings
    {
        public IndividualViewModel()
        {
            this.Friends = new HashSet<IndividualViewModel>();
            this.FriendRequests = new HashSet<IndividualViewModel>();
            this.Following = new HashSet<OrganizationViewModel>();

            this.Id = Guid.NewGuid();
        }

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string City { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        public ImageViewModel ProfileImage { get; set; }

        public IEnumerable<IndividualViewModel> Friends { get; set; }

        public IEnumerable<IndividualViewModel> FriendRequests { get; set; }

        public IEnumerable<OrganizationViewModel> Following { get; set; }

        //public IEnumerable<EventViewModel> EventsAttended { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Individual, IndividualViewModel>()
                .ForMember(viewModel => viewModel.City, cfg => cfg.MapFrom(model => model.User.City))
                .ForMember(viewModel => viewModel.UserName, cfg => cfg.MapFrom(model => model.User.UserName));
        }
    }
}