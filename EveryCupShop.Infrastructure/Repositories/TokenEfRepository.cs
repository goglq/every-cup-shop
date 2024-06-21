using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using EveryCupShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EveryCupShop.Infrastructure.Repositories;

public class TokenEfRepository : EfRepository<Token>, ITokenRepository
{
    public TokenEfRepository(AppDbContext context) : base(context) { }
    
    public Task<Token> Get(string refreshToken) => 
        Entities.FirstAsync(token => token.RefreshToken == refreshToken);

    public Task<Token> GetByUserId(Guid userId) => 
        Entities.FirstAsync(token => token.UserId == userId);

    public Task<Token?> Find(string refreshToken) => 
        Entities.FirstOrDefaultAsync(token => token.RefreshToken == refreshToken);

    public Task<Token?> FindByUserId(Guid userId) => 
        Entities.FirstOrDefaultAsync(token => token.UserId == userId);
}