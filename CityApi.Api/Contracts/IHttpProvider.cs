using CityApi.Models.Dtos;

namespace CityApi.Contracts;

public interface IHttpProvider
{
    public Task GetCountryDetailsAync(CityDto city);
    public Task GetWeatherAsync(CityDto city);
}