using Garden.Services;
using Garden.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace Garden.RouteGroups;

public static class GardenIdentityRouteGroup
{
   
    public static RouteGroupBuilder MapGardenIdentityRoutes(this RouteGroupBuilder group)
    {
        // group.MapGet("/", GetItem);
        group.MapPost("/", PostIdentity);
        // group.MapPut("/{id}", PutItem);
        // group.MapDelete("/{id}", DeleteItem);
        

        return group;
    }

    private static async Task<IResult> PostIdentity(UserManager<ApplicationUser> userManager, GardenService gardenService, User user)
    {
        ApplicationUser applicationUser = new ApplicationUser
        {
            UserName = user.Name,
            Email = user.Email
        };

        IdentityResult result = await userManager.CreateAsync(applicationUser, user.Password);

        if (result.Succeeded)
            return TypedResults.Created($"/{applicationUser.Id}", applicationUser);

        return TypedResults.Problem("Account could not be created");
    }
}