using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SupplementsShop.Application.DTOs;

namespace SupplementsShop.ViewModels;

public class PaymentViewModel
{
    [ValidateNever]
    public CartDto Cart { get; set; }
    [ValidateNever]
    public int? OrderNumber { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string CVV { get; set; }
    public string UserId { get; set; }
}