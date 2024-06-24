using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace EveryCupShop.Infrastructure.Repositories.CacheRepository;

public class OrderCachingEfRepository : CachingEfRepository<IOrderRepository, Order>, IOrderRepository
{
    public OrderCachingEfRepository(IOrderRepository repository, IDistributedCache cache) : base(repository, cache) { }
}