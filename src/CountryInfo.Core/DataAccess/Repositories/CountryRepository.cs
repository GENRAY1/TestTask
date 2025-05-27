using CountryInfo.Core.Abstractions.Repositories;
using CountryInfo.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CountryInfo.Core.DataAccess.Repositories;

internal class CountryRepository(ApplicationDbContext context)
    : ICountryRepository
{
    private readonly DbSet<Country> _dbSet = context.Set<Country>();

    public async Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken) =>
        await _dbSet.FirstOrDefaultAsync(e => e.Name == name, cancellationToken);

    public async Task AddAsync(Country country, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(country, cancellationToken);    
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => 
        await context.SaveChangesAsync(cancellationToken);
}