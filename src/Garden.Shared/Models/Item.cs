using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Garden.Shared.Models;

public enum NodeType
{
    Text,
    MultiLineText,
    Markdown
}

public record Node
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("type")]
    public NodeType Type { get; set; }
    
    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public record Item
{
    [JsonPropertyName("id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // [BsonElement("Name")]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("version")]
    public int? Version { get; set; }

    [JsonPropertyName("nodes")]
    public List<Node>? Nodes { get; set; }
}
