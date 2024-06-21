using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using EveryCupShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EveryCupShop.Infrastructure.Repositories;

public class UserEfRepository : EfRepository<User>, IUserRepository
{
    public UserEfRepository(AppDbContext context) : base(context)
    {
    }

    public Task<User> Get(string email) =>
        Entities.FirstAsync(user => user.Email == email);

    public Task<User?> Find(string email) =>
        Entities.FirstOrDefaultAsync(user => user.Email == email);
}