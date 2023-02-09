using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using Garden.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Garden.Admin.Services;

public class TokenService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ProtectedLocalStorage _protectedLocalStorage;
    private readonly NavigationManager _navigationManager;

    public TokenService(IHttpClientFactory httpClientFactory, ProtectedLocalStorage protectedLocalStorage, NavigationManager navigationManager)
    {
        _httpClientFactory = httpClientFactory;
        _protectedLocalStorage = protectedLocalStorage;
        _navigationManager = navigationManager;
    }

    public async Task CheckAuthentication()
    {
        try
        {
            await CheckTokenAsync();
        }
        catch (HttpRequestException e)
        {
            _navigationManager.NavigateTo("/logout");
        }
        catch (AuthenticationException e)
        {
            _navigationManager.NavigateTo(e.Message == "Not Authenticated" ? "/login" : "/logout");
        }
    }

    public async Task<bool> CheckTokenAsync()
    {
        var token = await GetToken();

        TokenDTO tokenDto = new TokenDTO(token);

        var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7161/garden/identity/validate");
        request.Content = new StringContent(JsonSerializer.Serialize(tokenDto), Encoding.UTF8, "application/json");

        var client = _httpClientFactory.CreateClient();

        var response = await client.SendAsync(request);
        await using var responseStream = await response.Content.ReadAsStreamAsync();

        if (response.IsSuccessStatusCode) return true;
        
        await _protectedLocalStorage.DeleteAsync("isAuthenticated");
        await _protectedLocalStorage.DeleteAsync("token");
        throw new HttpRequestException();

    }

    public async Task<string> GetToken()
    {
        var isAuthenticated = await _protectedLocalStorage.GetAsync<bool>("isAuthenticated");

        if (isAuthenticated is not { Success: true, Value: true }) throw new AuthenticationException("Not Authenticated");
        
        var tokenResult = await _protectedLocalStorage.GetAsync<string>("token");
        var token = tokenResult.Success ? tokenResult.Value : throw new AuthenticationException("No token found");

        return token!;
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