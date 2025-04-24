using SupplementsShop.ViewModels;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Factories;

public interface ICartModelFactory
{
     CartViewModel? PrepareCartViewModel(Cart? cart);
}