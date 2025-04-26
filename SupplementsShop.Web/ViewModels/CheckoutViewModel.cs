using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SupplementsShop.Web.ViewModels;

public class CheckoutViewModel
{
    [ValidateNever]
    public CartViewModel? Cart { get; set; }
    public OrderViewModel Order { get; set; }
}