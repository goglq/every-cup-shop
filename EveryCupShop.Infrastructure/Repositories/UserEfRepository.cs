using System.Linq.Expressions;
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

    public Task<User> Get(string email, params Expression<Func<User, object>>[] includes) =>
        GetQueryWithIncludes(includes).FirstAsync(user => user.Email == email);

    public Task<User?> Find(string email, params Expression<Func<User, object>>[] includes) =>
        GetQueryWithIncludes(includes).FirstOrDefaultAsync(user => user.Email == email);
}