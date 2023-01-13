using Garden.Models;
using Garden.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoStoreDatabaseSettings>("GardenStore", builder.Configuration.GetSection("GardenStoreDatabase"));

builder.Services.AddScoped<GardenService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/items", async (GardenService gardenService) => await gardenService.GetAsync());

app.Run();
