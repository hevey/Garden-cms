using Garden.Models;
using Garden.Services;

namespace Garden.RouteGroups;

public static class GardenItemRouteGroup
{
    public static RouteGroupBuilder MapGardenItemRoutes(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetItems);
        group.MapGet("/item", GetItem);

        return group;
    }

    private static async Task<IResult> GetItem(HttpContext context, GardenService gardenService, string? id, string? name)
    {
        if (id is not null)
            return Results.Ok(await gardenService.GetByIdAsync(id));
        if (name is not null)
            return Results.Ok(await gardenService.GetByNameAsync(name));

        return Results.BadRequest("Unable to return item. Provide either a 'id' or 'name' parameter");
    }

    private static async Task<List<Item>> GetItems(HttpContext context, GardenService gardenService, string? name)
    {
        if (name is not null)
            return await gardenService.GetAllAsync(name);
            
        return await gardenService.GetAllAsync();
    }
}