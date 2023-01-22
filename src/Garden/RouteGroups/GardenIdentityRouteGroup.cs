using System.Security.Claims;
using System.Security.Principal;
using Garden.Services;
using Garden.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Garden.RouteGroups;

public static class GardenIdentityRouteGroup
{
   
    public static RouteGroupBuilder MapGardenIdentityRoutes(this RouteGroupBuilder group)
    {
        // group.MapGet("/", GetItem);
        group.MapPost("/login", SignIn);
        // group.MapPost("/create", PostIdentity);
        // group.MapPut("/{id}", PutItem);
        // group.MapDelete("/{id}", DeleteItem);
        

        return group;
    }

    private static async Task<IResult> SignIn(UserManager<ApplicationUser> userManager, GardenService gardenService,
        TokenService tokenService, User user)
    {

        var result = await userManager.FindByNameAsync(user.Name);

        if (result is null)
        {
            return TypedResults.BadRequest("username or password is incorrect");
        }

        var passwordValid = await userManager.CheckPasswordAsync(result, user.Password);

        if (!passwordValid)
        {
            return TypedResults.BadRequest("username or password is incorrect"); 
        } 
        
        // signInManager.PasswordSignInAsync(user.Name, user.Password, true, false);
        //
        // if (!result.Succeeded) return TypedResults.BadRequest("Email and/or password is incorrect");
        //
        // var loggedInUser = signInManager.UserManager.Users.First(u => u.Email == user.Email);
    
        var token = tokenService.GenerateToken(result);
        
        return TypedResults.Ok(token);
    }
    
    // private static async Task<IResult> PostIdentity(UserManager<ApplicationUser> userManager, GardenService gardenService, User user)
    // {
    //     ApplicationUser applicationUser = new ApplicationUser
    //     {
    //         UserName = user.Name,
    //         Email = user.Email
    //     };
    //
    //     IdentityResult result = await userManager.CreateAsync(applicationUser, user.Password);
    //     
    //     
    //
    //     if (result.Succeeded)
    //         return TypedResults.Created($"/{applicationUser.Id}", applicationUser);
    //
    //     return TypedResults.Problem("Account could not be created");
    // }
}