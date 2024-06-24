using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public Task<IList<Role>> GetAll() => _roleRepository.GetAll();

    public Task<Role> Get(Guid id) => _roleRepository.Get(id);

    public Task<Role> Get(string name) => _roleRepository.Get(name);

    public async Task<Role> Create(string name)
    {
        var role = await _roleRepository.Find(name);

        if (role is not null)
            throw new DataAlreadyExistException();

        var newRole = new Role()
        {
            Name = name
        };

        var addedRole = await _roleRepository.Add(newRole);
        await _roleRepository.Save();

        return addedRole;
    }

    public async Task<Role> Put(Role role)
    {
        await _roleRepository.Update(role);
        await _roleRepository.Save();

        return role;
    }

    public async Task<Role> Delete(Guid id)
    {
        var role = await _roleRepository.Find(id);

        if (role is null)
            throw new DataNotFoundException();

        await _roleRepository.Delete(role);
        await _roleRepository.Save();
        
        return role;
    }
}