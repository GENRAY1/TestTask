using CountryInfo.Core;
using CountryInfo.Core.Abstractions.Common;
using CountryInfo.Core.Application.GetCountry;
using CountryInfo.Core.Domain;
using CountryInfo.Core.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "../../settings.json"));

string connectionString = 
    builder.Configuration.GetConnectionString("Db")
    ?? throw new InvalidOperationException("Connection string not found");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCoreServices(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Services.ApplyPendingMigrations();

app.MapGet("country", async (
    string name, 
    IQueryHandler<GetCountryQuery, Country> getCountryHandler,
    CancellationToken cancellationToken) => 
{
    try
    {
        Country country =
            await getCountryHandler.Handle(new GetCountryQuery(name), cancellationToken);

        return Results.Ok(country);
    }
    catch (CountryNotFoundException e)
    {
        return Results.NotFound();
    }
    catch (Exception e)
    {
        return Results.Problem(
            title: "An unexpected error occurred",
            detail: e.Message,
            statusCode: StatusCodes.Status500InternalServerError);
    }
});


app.Run();
