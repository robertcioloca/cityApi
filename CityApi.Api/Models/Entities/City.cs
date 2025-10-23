using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CityApi.Models.Entities;

public class City
{
    [Key]
    public long Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    [MaxLength(255)]
    public required string State { get; set; }
    [MaxLength(255)]
    public required string Country { get; set; }
    [Precision(3, 2)]
    [Range(1.0, 5.0)]
    public decimal Rating { get; set; }
    public DateOnly? DateEstablished { get; set; }
    public uint EstimatedPopulation { get; set; }
}
