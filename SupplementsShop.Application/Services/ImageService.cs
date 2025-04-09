using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace SupplementsShop.Application.Services;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public async Task<string> SaveImageAsync(IFormFile file, string? oldImagePath = null)
    {
        
    }
}