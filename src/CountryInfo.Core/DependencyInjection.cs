using CountryInfo.Core.Abstractions.Common;
using CountryInfo.Core.Abstractions.CountryService;
using CountryInfo.Core.Abstractions.Repositories;
using CountryInfo.Core.Application.GetCountry;
using CountryInfo.Core.DataAccess;
using CountryInfo.Core.DataAccess.Repositories;
using CountryInfo.Core.Domain;
using CountryInfo.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace CountryInfo.Core;

public static class DependencyInjection
{
    public static void AddCoreServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));
        
        services.AddHttpClient<ICountryService, CountryService>()
            .AddPolicyHandler(Policy.WrapAsync(
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))),
                Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30))
            ));

        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IQueryHandler<GetCountryQuery, Country>, GetCountryHandler>();
    }
    
    public static void ApplyPendingMigrations(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}