using AutoMapper;
using CityApi.Core.Models.Dtos;
using CityApi.Core.Models.Entities;

namespace CityApi.Core.MappingProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>()
                .ReverseMap();
            CreateMap<CreateCityDto, City>();
            CreateMap<UpdateCityDto, City>();
        }
    }
}