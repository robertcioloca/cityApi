using CityApi.Core.Contracts;
using CityApi.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityApi.Data;

public class CityContext : DbContext, ICityContext
{
    public CityContext(DbContextOptions<CityContext> options)
        : base(options)
    {
    }

    public DbSet<City> Cities { get; set; } = null!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
