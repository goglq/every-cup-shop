using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> Get(string name);
    
    Task<Role?> Find(string name);
}