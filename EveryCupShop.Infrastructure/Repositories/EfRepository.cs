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

    public async Task<IList<TEntity>> GetAll() => 
        await Entities.ToListAsync();

    public Task<TEntity> Get(Guid id) =>
        Entities.FirstAsync(item => item.Id == id);

    public Task<TEntity?> Find(Guid id) =>
        Entities.FirstOrDefaultAsync(item => item.Id == id);

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