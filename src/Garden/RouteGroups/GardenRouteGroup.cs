namespace Garden.RouteGroups;

public static class GardenRouteGroup
{
    public static RouteGroupBuilder MapGardenRoutes(this RouteGroupBuilder group)
    {
        group.MapGroup("/item").MapGardenItemRoutes().RequireAuthorization();
        group.MapGroup("/items").MapGardenItemsRoutes().RequireAuthorization();
        group.MapGroup("/identity").MapGardenIdentityRoutes();
        
        group.MapGet("/", () => "Welcome to Garden");
        

        return group;
    }
}