using CountryInfo.Core.Abstractions.Common;
using CountryInfo.Core.Domain;

namespace CountryInfo.Core.Application.GetCountry;

public record GetCountryQuery (string Name) 
    : IQuery<Country>;