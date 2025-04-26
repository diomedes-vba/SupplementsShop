using SupplementsShop.Web.ViewModels;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Web.Factories;

public interface ICartModelFactory
{
     CartViewModel? PrepareCartViewModel(Cart? cart);
}