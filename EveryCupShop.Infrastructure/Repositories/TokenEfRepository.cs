using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using EveryCupShop.Infrastructure.Database;

namespace EveryCupShop.Infrastructure.Repositories;

public class TokenEfRepository : EfRepository<Token>, ITokenRepository
{
    public TokenEfRepository(AppDbContext context) : base(context) { }
}