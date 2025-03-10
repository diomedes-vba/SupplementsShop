using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public class PaymentService : IPaymentService
{
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentDto payment)
    {
        await Task.Delay(1000);
        
        // For demo purposes, we'll assume payment is always successful.
        // In a real scenario, call your payment gateway API here.
        return new PaymentResult { Success = true };
    }
}