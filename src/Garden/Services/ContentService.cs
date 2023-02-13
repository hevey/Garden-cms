using Garden.Shared.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Garden.Services;

public class ContentService
{
    private readonly IMongoCollection<Content> _contentCollection;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly MongoStoreDatabaseSettings _gardenStore;
    
    public ContentService(IOptionsSnapshot<MongoStoreDatabaseSettings> options)
    {
        _gardenStore = options.Get("GardenStore");
        MongoClient mongoClient = new(_gardenStore.ConnectionString);
        
        _mongoDatabase = mongoClient.GetDatabase(_gardenStore.DatabaseName);

        _contentCollection = _mongoDatabase.GetCollection<Content>(_gardenStore.ContentCollection);
    }

    public async Task<List<Content>> GetByItemAsync(string itemId) =>
        await _contentCollection.Find(c => c.TypeId == itemId).ToListAsync();

    public async Task<List<Content>> GetAllAsync() => await _contentCollection.Find(_ => true).ToListAsync();

    public async Task<Content?> GetContentById(string id) => await _contentCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Content newContent)
    {
        IMongoCollection<Item> itemCollection = _mongoDatabase.GetCollection<Item>(_gardenStore.ItemCollection);
        IAsyncCursor<Item>? itemValid = await itemCollection.FindAsync(i => i.Id == newContent.TypeId);

        if (itemValid.Current is null)
            throw new InvalidDataException("ItemID is invalid");
        
        await _contentCollection.InsertOneAsync(newContent);
    }

    public async Task UpdateAsync(string id, Content updatedContent) =>
        await _contentCollection.FindOneAndReplaceAsync(c => c.Id == id, updatedContent);

    public async Task RemoveAsync(string id) => await _contentCollection.DeleteOneAsync(c => c.Id == id);
    
    public bool CheckIdValid(string id)
    {
        return ObjectId.TryParse(id, out _);
    }
}