using Microsoft.AspNetCore.Http;

namespace SupplementsShop.Application.Services;

public interface IImageService
{
    Task<string> SaveImageAsync(IFormFile file, string? oldImagePath = null);
}