using Garden.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Garden.Services;

public class GardenService
{
    private readonly IMongoCollection<Item> _itemCollection;
    
    public GardenService(IOptionsSnapshot<MongoStoreDatabaseSettings> options)
    {
        var gardenStore = options.Get("GardenStore");
        var mongoClient = new MongoClient(gardenStore.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(gardenStore.DatabaseName);

        _itemCollection = mongoDatabase.GetCollection<Item>(gardenStore.CollectionName);
    }

    public async Task<List<Item>> GetAllAsync() => await _itemCollection.Find(_ => true).ToListAsync();
    public async Task<List<Item>> GetAllAsync(string name) => await _itemCollection.Find(x => x.Name == name).ToListAsync();
    
    public async Task<Item?> GetByIdAsync(string id) => await _itemCollection.Find(x => x.Id == id.ToString()).FirstOrDefaultAsync();
    public async Task<Item?> GetByNameAsync(string name) => await _itemCollection.Find(x => x.Name == name).SortByDescending(x => x.Version).FirstOrDefaultAsync();

    public async Task CreateAsync(Item newItem) => await _itemCollection.InsertOneAsync(newItem);

    public async Task UpdateAsync(string id, Item updatedItem) => await _itemCollection.ReplaceOneAsync(x => x.Id == id, updatedItem);

    public async Task RemoveAsync(string id) => await _itemCollection.DeleteOneAsync(x => x.Id == id);
}