using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface IAuthService
{
    Task<(string accessToken, string refreshToken)> SignIn(string email, string password);

    Task<User> SignUp(string email, string password);

    Task SignOut(Guid userId);

    Task<(string accessToken, string refreshToken)> Refresh(string refreshToken);
}