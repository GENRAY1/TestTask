using CountryInfo.Core.Services;

namespace CountryInfo.Core.Abstractions.CountryService;

public interface ICountryService
{
    Task<CountryResponse> GetCountryByNameAsync(string name);
}