namespace Garden.Shared.Models;

public class MongoStoreDatabaseSettings
{
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    public string ItemCollection { get; set; }

    public string ContentCollection { get; set; }
}