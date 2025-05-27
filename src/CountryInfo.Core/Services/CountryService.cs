using System.Net.Http.Json;
using CountryInfo.Core.Abstractions.CountryService;
using CountryInfo.Core.Exceptions;

namespace CountryInfo.Core.Services;

public class CountryService(HttpClient httpClient) 
    : ICountryService
{
    private readonly Uri _baseAddress = new("https://restcountries.com/v3.1/");
    
    public async Task<CountryResponse> GetCountryByNameAsync(string name) 
    {
        var response = await httpClient.GetAsync($"{_baseAddress}name/{name}");
        response.EnsureSuccessStatusCode();
        
        List<CountryResponse>? countries =
            await response.Content.ReadFromJsonAsync<List<CountryResponse>>();
        
        if (countries is null || countries.Count == 0) 
            throw new CountryNotFoundException(name);
        
        return countries[0];
    }
    
}