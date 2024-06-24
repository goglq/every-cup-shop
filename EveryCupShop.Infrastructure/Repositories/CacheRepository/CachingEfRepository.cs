using System.Text.Json;
using EveryCupShop.Core.Interfaces;
using EveryCupShop.Core.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace EveryCupShop.Infrastructure.Repositories.CacheRepository;

public abstract class CachingEfRepository<TRepository, TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
    where TRepository : class, IRepository<TEntity> 
{
    protected TRepository Repository { get; }
    
    protected IDistributedCache Cache { get; }

    protected DistributedCacheEntryOptions Options { get; } = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
    };

    protected CachingEfRepository(TRepository repository, IDistributedCache cache)
    {
        Cache = cache;
        Repository = repository;
    }

    protected static string CacheKey(string? id = null) => $"Entity_{typeof(TEntity).Name}_{id ?? "All"}";

    protected async Task<TResult?> FindCachedItem<TResult>(string key)
    {
        var cachedItemsStr = await Cache.GetStringAsync(key);
        return !string.IsNullOrEmpty(cachedItemsStr) 
            ? JsonSerializer.Deserialize<TResult>(cachedItemsStr) 
            : default;
    }

    protected void InvalidateCache(string? key = null) => Cache.Remove(CacheKey(key));

    public async Task<IList<TEntity>> GetAll()
    {
        var key = CacheKey();
        var cachedItems = await FindCachedItem<IList<TEntity>>(key);

        if (cachedItems is not null)
            return cachedItems;

        var items = await Repository.GetAll();

        if (items.Count > 0)
            await Cache.SetStringAsync(key, JsonSerializer.Serialize(items), Options);

        return items;
    }

    public async Task<TEntity> Get(Guid id)
    {
        var key = CacheKey(id.ToString());
        var cachedItem = await FindCachedItem<TEntity>(key);
        
        if (cachedItem is not null)
            return cachedItem;

        var item = await Repository.Get(id);

        await Cache.SetStringAsync(key, JsonSerializer.Serialize(item), Options);

        return item;
    }

    public async Task<TEntity?> Find(Guid id)
    {
        var key = CacheKey(id.ToString());
        var cachedItem = await FindCachedItem<TEntity>(key);
        
        if (cachedItem is not null)
            return cachedItem;

        var item = await Repository.Find(id);

        if (item is not null)
            await Cache.SetStringAsync(key, JsonSerializer.Serialize(item), Options);

        return item;
    }

    public async Task<TEntity> Add(TEntity item)
    {
        var newItem = await Repository.Add(item);
        InvalidateCache();
        return newItem;
    }

    public async Task<TEntity> Update(TEntity item)
    {
        var newItem = await Repository.Update(item);
        InvalidateCache(newItem.Id.ToString());
        return newItem;
    }

    public async Task Delete(TEntity item)
    {
        await Repository.Delete(item);
        InvalidateCache();
    }

    public Task Save() => Repository.Save();
}