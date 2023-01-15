namespace Garden.RouteGroups;

public static class GardenRouteGroup
{
    public static RouteGroupBuilder MapGardenRoutes(this RouteGroupBuilder group)
    {
        group.MapGroup("/item").MapGardenItemRoutes();
        group.MapGroup("/items").MapGardenItemsRoutes();
        
        group.MapGet("/", () => "Welcome to Garden");
        

        return group;
    }
}