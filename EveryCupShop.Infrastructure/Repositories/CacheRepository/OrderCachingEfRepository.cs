using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace EveryCupShop.Infrastructure.Repositories.CacheRepository;

public class OrderCachingEfRepository : CachingEfRepository<OrderEfRepository, Order>, IOrderRepository
{
    public OrderCachingEfRepository(OrderEfRepository repository, IDistributedCache cache) : base(repository, cache) { }
}