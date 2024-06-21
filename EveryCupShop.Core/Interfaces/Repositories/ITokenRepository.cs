using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Repositories;

public interface ITokenRepository : IRepository<Token>
{
    Task<Token> Get(string refreshToken);

    Task<Token> GetByUserId(Guid userId);

    Task<Token?> Find(string refreshToken);

    Task<Token?> FindByUserId(Guid userId);
}