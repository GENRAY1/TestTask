using CountryInfo.Core;
using CountryInfo.Grps.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "../../settings.json"));

string connectionString = 
    builder.Configuration.GetConnectionString("Db")
    ?? throw new InvalidOperationException("Connection string not found");

builder.Services.AddGrpc();
builder.Services.AddCoreServices(connectionString);

var app = builder.Build();

app.Services.ApplyPendingMigrations();

app.MapGrpcService<CountryService>();

app.Run();
