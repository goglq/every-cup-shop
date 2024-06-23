using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using EveryCupShop.Infrastructure.Database;

namespace EveryCupShop.Infrastructure.Repositories;

public class CupShapeEfRepository : EfRepository<CupShape>, ICupShapeRepository
{
    public CupShapeEfRepository(AppDbContext context) : base(context) { }
}