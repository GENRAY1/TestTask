using System.Text.Json;
using CountryInfo.Core;
using CountryInfo.Core.Abstractions.Common;
using CountryInfo.Core.Application.GetCountry;
using CountryInfo.Core.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

string baseDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
Environment.CurrentDirectory = baseDirectory;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile(Path.Combine(baseDirectory, "../../settings.json"))
    .Build();

string connectionString = 
    configuration.GetConnectionString("Db")
    ?? throw new InvalidOperationException("Connection string not found");

services.AddCoreServices(connectionString);

ServiceProvider serviceProvider = services.BuildServiceProvider();
serviceProvider.ApplyPendingMigrations();

var getCountryHandler = serviceProvider.GetRequiredService<IQueryHandler<GetCountryQuery, Country>>()
    ?? throw new InvalidOperationException("GetCountryHandler not found");

while (true)
{
    Console.Write("> Enter country name (or 'q' to quit): ");
    
    string? input = Console.ReadLine();
    
    if (input?.ToLower() == "q") break;
    
    if (string.IsNullOrWhiteSpace(input)){
        Console.WriteLine("Please enter a country name");
        continue;
    }

    try
    {
        Country country = await getCountryHandler.Handle(new GetCountryQuery(input), CancellationToken.None);
        Console.WriteLine("\nCountry information:");
        Console.WriteLine(JsonSerializer.Serialize(country));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine("Please try another country name");
    }
    finally
    {
        Console.WriteLine();
    }
}



