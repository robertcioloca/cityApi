using System.Text.Json;
using AutoMapper;
using CityApi.Core.Contracts;
using CityApi.Core.Models.API;
using CityApi.Core.Models.Dtos;
using Microsoft.Extensions.Options;

namespace CityApi.Core.Providers;

public class HttpProvider(IOptions<CityApiCoreSettings> settings, IMapper mapper) : IHttpProvider
{
    private readonly CityApiCoreSettings _settings = settings.Value;
    private readonly IMapper _mapper = mapper;
    private readonly HttpClient _httpClient = new();
    private readonly JsonSerializerOptions serializerOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task GetCountryDetailsAync(CityDto city)
    {
        var response = await _httpClient.GetAsync($"{_settings.CountriesEndpoint}/name/{city.Country}");
        if (IsFailedStatus(response))
        {
            return;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<CityApiResponse>>(jsonResponse, serializerOptions);
        if (result?.Count > 0)
        {
            var item = result[0];
            city.TwoDigitCountryCode = item.CCA2;
            city.ThreeDigitCountryCode = item.Cioc;
            if (item.Currencies.Count > 0)
            {
                city.CurrencyCode = item.Currencies.Keys.First();
            }
        }
    }

    public async Task GetWeatherAsync(CityDto city)
    {
        var url = $"{_settings.WeatherEndpoint}?q={city.Name}&appid={_settings.OpenWeatherMapKey}&units=metric";
        var response = await _httpClient.GetAsync(url);
        if (IsFailedStatus(response))
        {
            return;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<WeatherApiResponse>(jsonResponse, serializerOptions);
        if (result != null)
        {
            city.Weather = _mapper.Map<WeatherApiResponse.MainInfo, WeatherDto>(result.Main);
        }
    }

    private static bool IsFailedStatus(HttpResponseMessage response)
    {
        return response.StatusCode == System.Net.HttpStatusCode.NotFound || !response.IsSuccessStatusCode;
    }
}