using AutoMapper;
using CityApi.Models.Dtos;
using CityApi.Models.API;

namespace CityApi.MappingProfiles
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<WeatherApiResponse.MainInfo, WeatherDto>()
                .ForMember(dest => dest.FeesLike, opt => opt.MapFrom(src => src.Feels_Like))
                .ForMember(dest => dest.TempMin, opt => opt.MapFrom(src => src.Temp_Min))
                .ForMember(dest => dest.TempMax, opt => opt.MapFrom(src => src.Temp_Max));
        }
    }
}