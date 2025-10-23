using System.Text.Json;
using AutoMapper;
using CityApi.Contracts;
using CityApi.Models.API;
using CityApi.Models.Dtos;

namespace CityApi.Providers;

public class HttpProvider(IConfiguration configuration, IMapper mapper) : IHttpProvider
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IMapper _mapper = mapper;
    private readonly HttpClient _httpClient = new();
    private readonly JsonSerializerOptions serializerOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task GetCountryDetailsAync(CityDto city)
    {
        var response = await _httpClient.GetAsync($"{_configuration["APIEndpoints:Countries"]}/name/{city.Country}");
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
        var url = $"{_configuration["APIEndpoints:Weather"]}?q={city.Name}&appid={_configuration["APIEndpoints:Keys:OpenWeatherMap"]}&units=metric";
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