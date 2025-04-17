using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public interface IInventoryApiClient
{
    Task<InventoryItemDto?> GetInventoryItemAsync(string productNumber);
    Task<IEnumerable<InventoryItemDto>> GetBatchInventoryItemsAsync(string[] productNumbers);
    Task UpdateInventoryItemAsync(InventoryItemDto inventoryItem);
}