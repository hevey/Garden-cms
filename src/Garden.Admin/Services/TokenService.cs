using System.Text;
using System.Text.Json;
using Garden.Shared.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Garden.Admin.Services;

public class TokenService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ProtectedLocalStorage _protectedLocalStorage;

    public TokenService(IHttpClientFactory httpClientFactory, ProtectedLocalStorage protectedLocalStorage)
    {
        _httpClientFactory = httpClientFactory;
        _protectedLocalStorage = protectedLocalStorage;
    }

    public async Task<bool> CheckTokenAsync()
    {
        var isAuthenticated = await _protectedLocalStorage.GetAsync<bool>("isAuthenticated");

        if (isAuthenticated is not { Success: true, Value: true }) return false;
        
        var tokenResult = await _protectedLocalStorage.GetAsync<string>("token");
        var token = tokenResult.Success ? tokenResult.Value : null;
        
        return token is not null;
    }

    public async Task<bool> Login(User user)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7161/garden/identity/login");
        request.Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

        var client = _httpClientFactory.CreateClient();

        var response = await client.SendAsync(request);
        await using var responseStream = await response.Content.ReadAsStreamAsync();
        
        var content = await JsonSerializer.DeserializeAsync<string>(responseStream);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(content);

        if (content != null) 
            await _protectedLocalStorage.SetAsync("token", content);
        
        await _protectedLocalStorage.SetAsync("isAuthenticated", true);

        return true;

    }
}