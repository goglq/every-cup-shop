using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using EveryCupShop.Infrastructure.Database;

namespace EveryCupShop.Infrastructure.Repositories;

public class OrderItemEfRepository : EfRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemEfRepository(AppDbContext context) : base(context) { }
}