using SupplementsShop.Domain.Models;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Factories;

public interface ICartModelFactory
{
     CartViewModel? PrepareCartViewModel(Cart? cart);
}