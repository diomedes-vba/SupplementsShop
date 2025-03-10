namespace SupplementsShop.Application.DTOs;

public class PaymentDto
{
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string CVV { get; set; }
}