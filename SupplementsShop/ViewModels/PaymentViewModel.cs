using SupplementsShop.Application.DTOs;

namespace SupplementsShop.ViewModels;

public class PaymentViewModel
{
    public CartDto Cart { get; set; }
    public string OrderNumber { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string CVV { get; set; }
}