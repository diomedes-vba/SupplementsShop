namespace SupplementsShop.ViewModels;
using Application.DTOs;

public class CheckoutViewModel
{
    public CartDto Cart { get; set; }
    public OrderDto Order { get; set; }
}