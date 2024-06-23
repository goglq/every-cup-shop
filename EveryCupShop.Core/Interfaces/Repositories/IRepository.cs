namespace EveryCupShop.Core.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<IList<TEntity>> GetAll();

    Task<TEntity> Get(Guid id);

    Task<TEntity?> Find(Guid id);
    
    Task<TEntity> Add(TEntity item);

    Task<TEntity> Update(TEntity item);

    Task Delete(TEntity item);

    Task Save();
}