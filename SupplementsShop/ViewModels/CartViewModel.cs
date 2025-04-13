namespace SupplementsShop.ViewModels;

public class CartViewModel
{
    public IList<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
    public decimal TotalPrice { get; set; }
}