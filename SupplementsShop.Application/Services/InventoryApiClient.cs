using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public class InventoryApiClient : IInventoryApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;
    private string _jwtToken;

    public InventoryApiClient(HttpClient httpClient, IAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    private async Task EnsureTokenAsync()
    {
        if (string.IsNullOrEmpty(_jwtToken))
        {
            _jwtToken = await _authService.GetTokenAsync();
        }
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
    }
    
    public async Task<InventoryItemDto?> GetInventoryItemAsync(string productNumber)
    {
        await EnsureTokenAsync();
        var response = await _httpClient.GetAsync($"/api/inventory/{productNumber}");
        response.EnsureSuccessStatusCode();
        var jsonResult = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<InventoryItemDto>(jsonResult);
    }
    
    public async Task<IEnumerable<InventoryItemDto>> GetBatchInventoryItemsAsync(string[] productNumbers)
    {
        await EnsureTokenAsync();
        
        var payload = JsonConvert.SerializeObject(productNumbers);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"/api/inventory/batch", content);
        
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<InventoryItemDto>>(json);
    }

    public async Task UpdateInventoryItemAsync(InventoryItemDto inventoryItem)
    {
        await EnsureTokenAsync();
        var json = JsonConvert.SerializeObject(inventoryItem);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"/api/inventory/{inventoryItem.ProductNumber}", content);
        response.EnsureSuccessStatusCode();
    }
}