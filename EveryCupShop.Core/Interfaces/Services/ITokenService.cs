using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface ITokenService
{
    (string accessToken, string refreshToken) GenerateTokens(User user);
}