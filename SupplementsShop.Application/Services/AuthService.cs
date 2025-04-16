using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SupplementsShop.Application.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task<string> GetTokenAsync()
    {
        var authUrl = "/api/auth/token";

        var credentials = new
        {
            Username = "testuser",
            Password = "testpassword"
        };
        
        var json = JsonConvert.SerializeObject(credentials);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(authUrl, content);
        response.EnsureSuccessStatusCode();
        
        var resultJson = await response.Content.ReadAsStringAsync();
        dynamic tokenResponse = JsonConvert.DeserializeObject(resultJson);
        return tokenResponse.token;
    }
}