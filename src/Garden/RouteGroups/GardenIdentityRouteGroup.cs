using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Garden.Services;
using Garden.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Garden.RouteGroups;

public static class GardenIdentityRouteGroup
{
   
    public static RouteGroupBuilder MapGardenIdentityRoutes(this RouteGroupBuilder group)
    {
        group.MapPost("/login", SignIn);
        group.MapPost("/create", PostIdentity).RequireAuthorization();

        return group;
    }

    private static async Task<IResult> SignIn(UserManager<ApplicationUser> userManager, GardenService gardenService,
        TokenService tokenService, User user)
    {

        var result = await userManager.FindByEmailAsync(user.Email);

        if (result is null)
        {
            return TypedResults.BadRequest("username or password is incorrect");
        }

        var passwordValid = await userManager.CheckPasswordAsync(result, user.Password);

        if (!passwordValid)
        {
            return TypedResults.BadRequest("username or password is incorrect"); 
        }

        var token = tokenService.GenerateToken(result);
        
        return TypedResults.Ok(token);
    }
    
    private static async Task<IResult> PostIdentity(UserManager<ApplicationUser> userManager, GardenService gardenService, User user)
    {
        ApplicationUser applicationUser = new ApplicationUser
        {
            Email = user.Email
        };
    
        IdentityResult result = await userManager.CreateAsync(applicationUser, user.Password);
        
        
        if (result.Succeeded)
            return TypedResults.Created($"/{applicationUser.Id}", applicationUser);

        var errorMessages = new StringBuilder();
        
        foreach (var identityError in result.Errors)
        {
            errorMessages.Append(identityError.Description);
        }
    
        return TypedResults.BadRequest(errorMessages.ToString());
    }
}