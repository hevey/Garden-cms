using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Garden.Shared.Models;
using Microsoft.IdentityModel.Tokens;

namespace Garden.Services;

public class TokenService
{
    public string? GenerateToken(ApplicationUser user)
    {
        var header = new JwtHeader(
            new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_Secret") ?? throw new InvalidOperationException("Missing JWT_Secret environment variable"))), 
                SecurityAlgorithms.HmacSha256));

        if (user.Email is null || user.UserName is null) return null;
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Exp,
                new DateTimeOffset(DateTime.Now.AddHours(6)).ToUnixTimeSeconds().ToString()),
            new(JwtRegisteredClaimNames.Iss, "local-auth"),
            new(JwtRegisteredClaimNames.Aud, "http://localhost:17635"),
            new(JwtRegisteredClaimNames.Aud, "https://localhost:44389"),
            new(JwtRegisteredClaimNames.Aud, "http://localhost:5104"),
            new(JwtRegisteredClaimNames.Aud, "https://localhost:7161")
        };

        var payload = new JwtPayload(claims);

        var token = new JwtSecurityToken(header, payload);
 
        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    public bool Validate(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();
        SecurityToken validatedToken;
        IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        return true;
    }
    
    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true, 
            ValidIssuer = "local-auth",
            ValidAudiences = new List<string>{ "http://localhost:17635", "https://localhost:44389", "http://localhost:5104", "https://localhost:7161" },
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_Secret") ?? throw new InvalidOperationException("Missing JWT_Secret environment variable"))) // The same key as the one that generate the token
        };
    }
}