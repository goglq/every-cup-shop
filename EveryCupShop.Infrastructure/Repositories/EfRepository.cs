using System.Linq.Expressions;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EveryCupShop.Infrastructure.Repositories;

public abstract class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly AppDbContext _context;

    protected DbSet<TEntity> Entities => _context.Set<TEntity>();

    protected EfRepository(AppDbContext context)
    {
        _context = context;
    }

    protected IQueryable<TEntity> GetQueryWithIncludes(IReadOnlyCollection<Expression<Func<TEntity, object>>> includes) =>
        includes.Count > 0 
            ? includes
                .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(
                    Entities, 
                    (current, include) => current.Include(include))
            : Entities;

    public async Task<IList<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes)
    {
        return await GetQueryWithIncludes(includes).ToListAsync();
    }

    public Task<TEntity> Get(Guid id, params Expression<Func<TEntity, object>>[] includes) =>
        GetQueryWithIncludes(includes).FirstAsync(item => item.Id == id);

    public Task<TEntity?> Find(Guid id, params Expression<Func<TEntity, object>>[] includes) =>
        GetQueryWithIncludes(includes).FirstOrDefaultAsync(item => item.Id == id);

    public async Task<TEntity> Add(TEntity item)
    {
        var entry = await Entities.AddAsync(item);
        
        return entry.Entity;
    }

    public Task<TEntity> Update(TEntity item)
    {
        var entry = _context.Entry(item);
        entry.State = EntityState.Modified;
        return Task.FromResult(entry.Entity);
    }
    
    public Task Delete(TEntity item)
    {
        Entities.Remove(item);
        return Task.CompletedTask;;
    }

    public Task Save() =>
        _context.SaveChangesAsync();
}