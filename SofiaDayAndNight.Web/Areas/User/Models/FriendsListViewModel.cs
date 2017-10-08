using System;

using AutoMapper;

using SofiaDayAndNight.Web.Infrastructure;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Web.Models;

namespace SofiaDayAndNight.Web.Areas.User.Models
{
    public class FriendsListViewModel : IMapFrom<Individual>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public int Age { get; set; }

        public ImageViewModel ProfileImage { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Individual, IndividualViewModel>()
                .ForMember(viewModel => viewModel.City, cfg => cfg.MapFrom(model => model.User.City));
        }
    }
}