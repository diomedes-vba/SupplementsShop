using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public interface IInventoryApiClient
{
    Task<InventoryItemDto?> GetInventoryItemAsync(string productNumber);
    Task UpdateInventoryItemAsync(InventoryItemDto inventoryItem);
}