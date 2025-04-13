using SupplementsShop.Application.DTOs;

namespace SupplementsShop.Application.Services;

public class PaymentService : IPaymentService
{
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentDto payment)
    {
        await Task.Delay(1000);
        
        // For demo purposes, every payment is successful
        // Payment API call here
        return new PaymentResult { Success = true };
    }
}