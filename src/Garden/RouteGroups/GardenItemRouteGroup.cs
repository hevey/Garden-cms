using Garden.Models;
using Garden.Services;
using MongoDB.Bson;

namespace Garden.RouteGroups;

public static class GardenItemRouteGroup
{
    public static RouteGroupBuilder MapGardenItemRoutes(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetItem);
        group.MapPost("/", PostItem);
        group.MapPut("/{id}", PutItem);
        group.MapDelete("/{id}", DeleteItem);
        

        return group;
    }

    private static async Task<IResult> GetItem(HttpContext context, GardenService gardenService, string? id, string? name)
    {
        if (id is not null)
            return TypedResults.Ok(await gardenService.GetByIdAsync(id));
        if (name is not null)
            return TypedResults.Ok(await gardenService.GetByNameAsync(name));

        return Results.BadRequest("Unable to return item. Provide either a 'id' or 'name' parameter");
    }
    
    private static async Task<IResult> PostItem(GardenService gardenService, Item item)
    {
        item.Version = 1;
        
        await gardenService.CreateAsync(item);
        return TypedResults.Created($"/{item.Id}", item);
    }
    
    private static async Task<IResult> PutItem(GardenService gardenService, string id, Item item)
    {

        var isIdValid = ObjectId.TryParse(id, out _);

        if (!isIdValid)
            return TypedResults.BadRequest("id is not valid");
        
        var result = await gardenService.GetByIdAsync(id);

        if (result is null)
            return TypedResults.NotFound();

        result.Name = item.Name;
        result.Nodes = item.Nodes;
        result.Version++;
        
        await gardenService.UpdateAsync(id, result);
        
        return TypedResults.NoContent();
    }
    
    private static async Task<IResult> DeleteItem(GardenService gardenService, string id)
    {
        var isIdValid = ObjectId.TryParse(id, out _);

        if (!isIdValid)
            return TypedResults.BadRequest("id is not valid");
        
        var result = await gardenService.GetByIdAsync(id);

        if (result is not Item) return TypedResults.NotFound();
        
        await gardenService.RemoveAsync(id);
        return TypedResults.Ok(result);
    }
}