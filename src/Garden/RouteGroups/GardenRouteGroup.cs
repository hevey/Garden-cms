namespace Garden.RouteGroups;

public static class GardenRouteGroup
{
    public static RouteGroupBuilder MapGardenRoutes(this RouteGroupBuilder group)
    {
        group.MapGroup("/items").MapGardenItemRoutes();
        
        group.MapGet("/", () => "Welcome to Garden");
        

        return group;
    }
}