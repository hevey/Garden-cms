using Garden.Models;
using Garden.Services;
using Garden.RouteGroups;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoStoreDatabaseSettings>("GardenStore", builder.Configuration.GetSection("GardenStoreDatabase"));
builder.Services.AddScoped<GardenService>();

var app = builder.Build();

app.MapGroup("/garden").MapGardenRoutes();

app.Run();
