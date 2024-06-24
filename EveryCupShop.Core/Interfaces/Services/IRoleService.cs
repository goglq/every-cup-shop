using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface IRoleService
{
    Task<IList<Role>> GetAll();

    Task<Role> Get(Guid id);
    
    Task<Role> Get(string name);
    
    Task<Role> Create(string name);

    Task<Role> Put(Role role);

    Task<Role> Delete(Guid id);
}