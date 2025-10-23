namespace CityApi.Models.Dtos;

public class CountryDto
{
    public long Id { get; set; }
    public required string Code { get; set; }
    public required string CurrencyCode { get; set; }
}
