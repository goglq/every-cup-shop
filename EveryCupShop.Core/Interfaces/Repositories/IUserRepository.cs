using System.Linq.Expressions;
using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> Get(string email, params Expression<Func<User, object>>[] includes);

    Task<User?> Find(string email, params Expression<Func<User, object>>[] includes);
}