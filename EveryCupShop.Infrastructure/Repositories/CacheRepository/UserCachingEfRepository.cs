using System.Linq.Expressions;
using System.Text.Json;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace EveryCupShop.Infrastructure.Repositories.CacheRepository;

public class UserCachingEfRepository : CachingEfRepository<IUserRepository, User>, IUserCachingRepository
{
    public UserCachingEfRepository(IUserRepository repository, IDistributedCache cache) : base(repository, cache)
    {
    }

    public async Task<User> Get(string email, params Expression<Func<User, object>>[] includes)
    {
        var key = CacheKey(email);
        var cachedItem = await FindCachedItem<User>(key);

        if (cachedItem is not null)
            return cachedItem;

        var item = await Repository.Get(email, includes);

        await Cache.SetStringAsync(key, JsonSerializer.Serialize(item), Options);

        return item;
    }

    public async Task<User?> Find(string email, params Expression<Func<User, object>>[] includes)
    {
        var key = CacheKey(email);
        var cachedItem = await FindCachedItem<User>(key);

        if (cachedItem is not null)
            return cachedItem;

        var item = await Repository.Find(email, includes);

        if (item is not null)
            await Cache.SetStringAsync(key, JsonSerializer.Serialize(item), Options);

        return item;
    }
}