namespace SupplementsShop.Application.Services;

public interface IAuthService
{
    Task<string> GetTokenAsync();
}