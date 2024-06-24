using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using EveryCupShop.Infrastructure.Database;

namespace EveryCupShop.Infrastructure.Repositories;

public class CupEfRepository : EfRepository<Cup>, ICupRepository
{
    public CupEfRepository(AppDbContext context) : base(context) { }
}