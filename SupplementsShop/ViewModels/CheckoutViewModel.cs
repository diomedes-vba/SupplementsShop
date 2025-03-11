using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SupplementsShop.ViewModels;
using Application.DTOs;

public class CheckoutViewModel
{
    [ValidateNever]
    public CartDto Cart { get; set; }
    public OrderDto Order { get; set; }
}