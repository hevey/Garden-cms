using Garden.Services;
using Garden.RouteGroups;
using Garden.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoStoreDatabaseSettings>("GardenStore", builder.Configuration.GetSection("GardenStoreDatabase"));
builder.Services.AddScoped<GardenService>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
    builder.Configuration.GetValue<string>("GardenStoreDatabase:ConnectionString"),
    builder.Configuration.GetValue<string>("GardenStoreDatabase:DatabaseName")
);


builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGroup("/garden").MapGardenRoutes().RequireAuthorization();
app.MapGroup("/identity").MapGardenIdentityRoutes();

app.Run();
