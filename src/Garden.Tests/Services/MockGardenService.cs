using Garden.Services;
using Garden.Shared.Models;

namespace Garden.Tests.Services;

public class MockGardenService : IGardenService
{
    private readonly List<Item> items;

    public MockGardenService()
    {
        items = new List<Item>();
    }

    public Task<List<Item>> GetAllAsync()
    {
        return Task.FromResult(items);
    }

    public Task<List<Item>> GetAllAsync(string name)
    {
        List<Item> filteredItems = items.FindAll(i => i.Name == name);
        return Task.FromResult(filteredItems);
    }

    public Task<List<Item>> GetAllAsync(bool latest)
    {
        List<Item> filteredItems = items.GroupBy(i => i.Name).Select(i => i.MaxBy(x => x.Version)).ToList()!;
        return Task.FromResult(filteredItems);
    }

    public Task<Item?> GetByIdAsync(string id)
    {
        return Task.FromResult(items.Find(i => i.Id == id));
    }

    public Task<Item?> GetByNameAsync(string name)
    {
        return Task.FromResult(items.Find(i => i.Name == name));
    }

    public Task CreateAsync(Item newItem)
    {
        items.Insert(items.Count, newItem);
        return Task.FromResult(items);
    }

    public Task UpdateAsync(string id, Item updatedItem)
    {
        int index = items.FindIndex(i => i.Id == id);
        if (index != -1)
            items[index] = updatedItem;
        return Task.FromResult(items);
    }

    public Task RemoveAsync(string id)
    {
        int index = items.FindIndex(i => i.Id == id);
        if(index != 1)
            items.RemoveAt(index);

        return Task.FromResult(items);
    }

    public bool CheckIdValid(string id)
    {
        //TODO: This needs to actually check if id is valid
        return true;
    }
}