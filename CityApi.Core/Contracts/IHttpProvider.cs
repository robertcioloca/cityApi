using CityApi.Core.Models.Dtos;

namespace CityApi.Core.Contracts;

public interface IHttpProvider
{
    public Task GetCountryDetailsAync(CityDto city);
    public Task GetWeatherAsync(CityDto city);
}