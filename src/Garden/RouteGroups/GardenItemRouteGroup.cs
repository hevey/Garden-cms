using Garden.Models;
using Garden.Services;

namespace Garden.RouteGroups;

public static class GardenItemRouteGroup
{
    public static RouteGroupBuilder MapGardenItemRoutes(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (GardenService gardenService) => await gardenService.GetAsync());

        return group;
    }


}