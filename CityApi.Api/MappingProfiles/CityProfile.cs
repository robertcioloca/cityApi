using AutoMapper;
using CityApi.Models.Entities;
using CityApi.Models.Dtos;

namespace CityApi.MappingProfiles
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