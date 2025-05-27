using CountryInfo.Core.Abstractions.Common;
using CountryInfo.Core.Abstractions.Repositories;
using CountryInfo.Core.Domain;
using CountryInfo.Core.Services;

namespace CountryInfo.Core.Application.GetCountry;

internal class GetCountryHandler(
    ICountryRepository countryRepository,
    ICountryService countryService
    ) : IQueryHandler<GetCountryQuery, Country>
{
    public async Task<Country> Handle(GetCountryQuery request, CancellationToken cancellationToken = default)
    {
        DateTime currentDate = DateTime.UtcNow;

        Country? country =
            await countryRepository.GetByNameAsync(request.Name, cancellationToken);

        if (country is null)
        {
            CountryResponse response = 
                await countryService.GetCountryByNameAsync(request.Name);

            Guid countryId = Guid.NewGuid();
            
            country = new Country
            {
                Id = countryId,
                Code = response.Cca2,
                Name = response.Name.Common,
                Region = response.Region,
                Subregion = response.Subregion,
                Capital = response.Capital.FirstOrDefault() ?? string.Empty,
                Population = response.Population,
                Currency = response.Currencies.Any() 
                        ? response.Currencies.First().Value.Name
                        : string.Empty,
                Statistic = new CountryStatistic
                {
                    AddedAt = currentDate
                }
            };

            await countryRepository.AddAsync(country, cancellationToken);
        }

        country.Statistic.TotalRequests++;
        country.Statistic.LastRequestedAt = currentDate;

        await countryRepository.SaveChangesAsync(cancellationToken);

        return country;
    }
}