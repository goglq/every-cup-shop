using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> Get(string email);

    Task<User?> Find(string email);
}