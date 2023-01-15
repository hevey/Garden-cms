using Garden.Models;
using Garden.Services;

namespace Garden.RouteGroups;

public static class GardenItemsRouteGroup
{
    public static RouteGroupBuilder MapGardenItemsRoutes(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetItems);

        return group;
    }

    private static async Task<IResult> GetItems(HttpContext context, GardenService gardenService, string? name)
    {
        if (name is not null) 
            return Results.Ok(await gardenService.GetAllAsync(name));
            
        return Results.Ok(await gardenService.GetAllAsync());
    }
}