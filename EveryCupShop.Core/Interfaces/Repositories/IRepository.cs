namespace EveryCupShop.Core.Interfaces.Repositories;

public interface IRepository<T> where T : class, IEntity
{
    Task<IList<T>> GetAll();

    Task<T> Get(Guid id);

    Task<T?> Find(Guid id);
    
    Task<T> Add(T item);

    void Update(T item);

    void Delete(T item);

    Task Save();
}