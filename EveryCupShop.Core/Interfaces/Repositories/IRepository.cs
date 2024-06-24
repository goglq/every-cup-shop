using System.Linq.Expressions;

namespace EveryCupShop.Core.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<IList<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes);

    Task<TEntity> Get(Guid id, params Expression<Func<TEntity, object>>[] includes);

    Task<TEntity?> Find(Guid id, params Expression<Func<TEntity, object>>[] includes);
    
    Task<TEntity> Add(TEntity item);

    Task<TEntity> Update(TEntity item);

    Task Delete(TEntity item);

    Task Save();
}