namespace SupplementsShop.Application.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Slug { get; set; }
    
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
}