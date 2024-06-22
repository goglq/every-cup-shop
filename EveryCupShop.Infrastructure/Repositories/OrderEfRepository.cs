using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using EveryCupShop.Infrastructure.Database;

namespace EveryCupShop.Infrastructure.Repositories;

public class OrderEfRepository : EfRepository<Order>, IOrderRepository
{
    public OrderEfRepository(AppDbContext context) : base(context) { }
}