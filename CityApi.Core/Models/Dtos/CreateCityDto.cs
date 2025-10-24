using System.ComponentModel.DataAnnotations;

namespace CityApi.Core.Models.Dtos;

public class CreateCityDto
{
    [MaxLength(255)]
    public required string Name { get; set; }
    [MaxLength(255)]
    public required string State { get; set; }
    [MaxLength(255)]
    public required string Country { get; set; }
    [Range(1.0, 5.0)]
    public decimal Rating { get; set; }
    public DateOnly DateEstablished { get; set; }
    public uint EstimatedPopulation { get; set; }
}

