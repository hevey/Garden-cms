using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Garden.Models;

public enum NodeType
{
    Text,
    MultiLineText,
    Markdown
}

public record Node(string Name, NodeType Type, string Value);

public record Item
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // [BsonElement("Name")]
    public string? Name { get; set; }

    public int? Version { get; set; }

    public List<Node>? Nodes { get; set; }
}
