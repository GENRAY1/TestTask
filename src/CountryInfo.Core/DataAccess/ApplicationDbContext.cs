using CountryInfo.Core.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CountryInfo.Core.DataAccess;

internal class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
    }
}
