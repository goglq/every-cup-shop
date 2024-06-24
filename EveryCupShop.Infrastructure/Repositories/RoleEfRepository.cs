using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using EveryCupShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EveryCupShop.Infrastructure.Repositories;

public class RoleEfRepository : EfRepository<Role>, IRoleRepository
{
    public RoleEfRepository(AppDbContext context) : base(context) { }

    public Task<Role> Get(string name) => Entities.FirstAsync(role => role.Name == name);

    public Task<Role?> Find(string name) => Entities.FirstOrDefaultAsync(role => role.Name == name);
}