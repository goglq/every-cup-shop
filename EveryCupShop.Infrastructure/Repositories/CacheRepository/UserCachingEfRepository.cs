using System.Text.Json;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace EveryCupShop.Infrastructure.Repositories.CacheRepository;

public class UserCachingEfRepository : CachingEfRepository<UserEfRepository, User>, IUserRepository
{
    public UserCachingEfRepository(UserEfRepository repository, IDistributedCache cache) : base(repository, cache)
    {
    }

    public async Task<User> Get(string email)
    {
        var key = CacheKey(email);
        var cachedItem = await FindCachedItem<User>(key);

        if (cachedItem is not null)
            return cachedItem;

        var item = await Repository.Get(email);

        await Cache.SetStringAsync(key, JsonSerializer.Serialize(item), Options);

        return item;
    }

    public async Task<User?> Find(string email)
    {
        var key = CacheKey(email);
        var cachedItem = await FindCachedItem<User>(key);

        if (cachedItem is not null)
            return cachedItem;

        var item = await Repository.Find(email);

        if (item is not null)
            await Cache.SetStringAsync(key, JsonSerializer.Serialize(item), Options);

        return item;
    }
}