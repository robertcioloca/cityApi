namespace CityApi.Core;

public class CityApiCoreSettings
{
    public required string CountriesEndpoint { get; set; } = string.Empty;
    public required string WeatherEndpoint { get; set; } = string.Empty;
    public required string OpenWeatherMapKey { get; set; } = string.Empty;
}