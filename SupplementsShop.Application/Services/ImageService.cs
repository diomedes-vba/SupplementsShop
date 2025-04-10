using Microsoft.AspNetCore.Http;

namespace SupplementsShop.Application.Services;

public class ImageService : IImageService
{
    private readonly string _webRootPath;

    public ImageService(string webRootPath)
    {
        _webRootPath = webRootPath;
    }
    public async Task<string> SaveImageAsync(IFormFile? file, string? oldImagePath = null)
    {
        if (file == null || file.Length == 0)
        {
            return oldImagePath ?? string.Empty;
        }
        
        var fileName = Path.GetFileName(file.FileName);
        var folderPath = Path.Combine(_webRootPath, "images", "products");
        var filePath = Path.Combine(folderPath, fileName);
        
        Directory.CreateDirectory(folderPath);
        
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return "/images/products/" + fileName;
    }
}