using Garden.Services;

namespace Garden.RouteGroups;

public static class GardenItemsRouteGroup
{
    public static RouteGroupBuilder MapGardenItemsRoutes(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetItems);
        group.MapGet("/latest", GetLatestItems);

        return group;
    }

    private static async Task<IResult> GetLatestItems(HttpContext context, IGardenService gardenService)
    {
        return TypedResults.Ok(await gardenService.GetAllAsync(true));
    }

    private static async Task<IResult> GetItems(HttpContext context, IGardenService gardenService, string? name)
    {
        return name is not null ? TypedResults.Ok(await gardenService.GetAllAsync(name)) : TypedResults.Ok(await gardenService.GetAllAsync());
    }
    
    
}