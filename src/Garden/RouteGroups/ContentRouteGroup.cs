using Garden.Services;
using Garden.Shared.Models;

namespace Garden.RouteGroups;

public static class ContentRouteGroup
{
    public static RouteGroupBuilder MapContentRoutes(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetMultipleContentItems);
        group.MapGet("/{id}", GetContent);
        
        return group;
    }

    private static async Task<IResult> GetMultipleContentItems(HttpContext context, ContentService contentService, string? typeId)
    {
        if (typeId is null) return TypedResults.Ok(await contentService.GetAllAsync());
        
        bool isTypeIdValid = contentService.CheckIdValid(typeId);

        if (!isTypeIdValid)
            return TypedResults.BadRequest("typeid id not valid");

        List<Content> result = await contentService.GetByItemAsync(typeId);

        if (result.Count is 0)
            return TypedResults.BadRequest($"no content with TypeId: {typeId} found");

        return TypedResults.Ok(result);

    }

    private static async Task<IResult> GetContent(HttpContext context, ContentService contentService, string id)
    {
        bool isIdValid = contentService.CheckIdValid(id);

        if (!isIdValid)
            return TypedResults.BadRequest("id id not valid");

        Content? result = await contentService.GetContentById(id);

        if (result is null)
            return TypedResults.BadRequest($"no content with id: {id} found");

        return TypedResults.Ok(result);
    }
}