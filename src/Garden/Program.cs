using Garden.Services;
using Garden.RouteGroups;
using Garden.Shared.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoStoreDatabaseSettings>("GardenStore", builder.Configuration.GetSection("GardenStoreDatabase"));
builder.Services.AddScoped<GardenService>();

builder.Services.AddSingleton<TokenService>();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<ApplicationUser>().AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
    builder.Configuration.GetValue<string>("GardenStoreDatabase:ConnectionString"),
    builder.Configuration.GetValue<string>("GardenStoreDatabase:DatabaseName")
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => 
    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    })
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("/garden").MapGardenRoutes().WithOpenApi();

app.Run();
