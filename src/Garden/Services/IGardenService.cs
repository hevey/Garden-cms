using Garden.Shared.Models;

namespace Garden.Services;

public interface IGardenService
{
    public Task<List<Item>> GetAllAsync();
    public Task<List<Item>> GetAllAsync(string name);
    public Task<List<Item>> GetAllAsync(bool latest);
    public Task<Item?> GetByIdAsync(string id);
    public Task<Item?> GetByNameAsync(string name);
    public Task CreateAsync(Item newItem);
    public Task UpdateAsync(string id, Item updatedItem);
    public Task RemoveAsync(string id);
    public bool CheckIdValid(string id);
}