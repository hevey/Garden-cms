namespace Garden.RouteGroups;

public static class GardenRouteGroup
{
    public static RouteGroupBuilder MapGardenRoutes(this RouteGroupBuilder group)
    {
        group.MapGroup("/item").MapGardenItemRoutes().RequireAuthorization().WithTags("Item");
        group.MapGroup("/items").MapGardenItemsRoutes().RequireAuthorization().WithTags("Items");
        group.MapGroup("/identity").MapGardenIdentityRoutes().WithTags("Identity");


        return group;
    }
}