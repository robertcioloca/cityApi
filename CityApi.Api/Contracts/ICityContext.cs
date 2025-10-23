using CityApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityApi.Contracts;

public interface ICityContext
{
    public DbSet<City> Cities { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
