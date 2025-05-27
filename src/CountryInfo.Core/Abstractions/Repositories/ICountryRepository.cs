using CountryInfo.Core.Domain;

namespace CountryInfo.Core.Abstractions.Repositories;

public interface ICountryRepository
{ 
    Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken);
    
    Task AddAsync(Country country, CancellationToken cancellationToken);
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}