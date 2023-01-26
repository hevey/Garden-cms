using System.Text;
using Garden.Services;
using Garden.RouteGroups;
using Garden.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoStoreDatabaseSettings>("GardenStore", builder.Configuration.GetSection("GardenStoreDatabase"));
builder.Services.AddScoped<GardenService>();

builder.Services.AddSingleton<TokenService>();


builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
    builder.Configuration.GetValue<string>("GardenStoreDatabase:ConnectionString"),
    builder.Configuration.GetValue<string>("GardenStoreDatabase:DatabaseName")
);

builder.Services.AddAuthentication("LocalAuthIssuer")
    .AddJwtBearer()
    .AddJwtBearer("LocalAuthIssuer", options =>
    {
        options.TokenValidationParameters.ValidIssuer = "local-auth";
        options.TokenValidationParameters.ValidateIssuerSigningKey = true;
        options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            Environment.GetEnvironmentVariable("JWT_Secret") ??
            throw new InvalidOperationException("Missing JWT_Secret environment variable")));
        options.TokenValidationParameters.ValidateIssuer = true;
        options.TokenValidationParameters.ValidateAudience = true;
        options.TokenValidationParameters.ValidateLifetime = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes("Bearer", "LocalAuthIssuer")
        .Build();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    setup.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        }
    );
});

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(policyBuilder =>
    {
        policyBuilder
            .WithOrigins("https://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
}

app.MapGroup("/garden").MapGardenRoutes().WithOpenApi();

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.Run();
