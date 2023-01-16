using Garden.Models;
using Garden.Services;
using Garden.RouteGroups;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoStoreDatabaseSettings>("GardenStore", builder.Configuration.GetSection("GardenStoreDatabase"));
builder.Services.AddScoped<GardenService>();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGroup("/garden").MapGardenRoutes().RequireAuthorization();

app.Run();
