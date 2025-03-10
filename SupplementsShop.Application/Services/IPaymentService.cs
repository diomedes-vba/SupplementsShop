using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public interface IPaymentService
{
    Task<PaymentResult> ProcessPaymentAsync(PaymentDto paymentDto);
}

public class PaymentResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}