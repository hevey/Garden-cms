using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Garden.Shared.Models;
using Microsoft.IdentityModel.Tokens;

namespace Garden.Services;

public class TokenService
{
    public string GenerateToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Exp, 
                new DateTimeOffset(DateTime.Now.AddHours(6))
                    .ToUnixTimeSeconds().ToString())
        };
 
        var header = new JwtHeader(
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_Secret") ?? throw new InvalidOperationException("Missing JWT_Secret environment variable"))), 
                SecurityAlgorithms.HmacSha256));
 
        
        var payload = new JwtPayload(claims);
 
        var token = new JwtSecurityToken(header, payload);
 
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}