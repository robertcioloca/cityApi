using System.Text.Json.Serialization;

namespace CityApi.Core.Models.API;

public class CityApiResponse
{
    public required NameInfo Name { get; set; }
    public required string CCA2 { get; set; }
    public required string Cioc { get; set; }
    [JsonPropertyName("currencies")]
    public required Dictionary<string, CurrencyInfo> Currencies { get; set; } = [];

    public class NameInfo
    {
        public required string Common { get; set; }
    }

    public class CurrencyInfo
    {
        public required string Name { get; set; }
        public required string Symbol { get; set; }
    }
}
