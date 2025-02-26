namespace SupplementsShop.Application.DTOs;

public class CartDto
{
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    public decimal TotalPrice { get; set; }
}