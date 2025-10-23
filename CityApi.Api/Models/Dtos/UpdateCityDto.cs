using System.ComponentModel.DataAnnotations;

namespace CityApi.Models.Dtos;

public class UpdateCityDto
{
    public long Id { get; set; }
    [Range(1.0, 5.0)]
    public decimal Rating { get; set; }
    public DateOnly DateEstablished { get; set; }
    public uint EstimatedPopulation { get; set; }
}
