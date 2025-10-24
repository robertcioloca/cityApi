using CityApi.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityApi.Core.Contracts;

public interface ICityContext
{
    public DbSet<City> Cities { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
