using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Garden.Shared.Models;

public record Content
{
    [JsonPropertyName("id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = default!;

    [JsonPropertyName("typeid")]
    public string TypeId { get; init; } = default!;
    
    [JsonPropertyName("nodes")]
    public List<Node> Nodes { get; init; } = default!;
}