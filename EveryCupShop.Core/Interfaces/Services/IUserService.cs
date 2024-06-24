using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface IUserService
{
    Task<User> CreateUser(string email, string password);

    Task<IList<User>> GetUsers();
    
    Task<User> GetUser(string email);
    
    Task<User> GetUser(Guid id);

    Task<bool> CheckEmail(string email);

    Task<User> Change(User user);

    Task<User> ChangeEmail(Guid id, string email);

    Task ChangePassword(Guid id, string password);

    Task<User> Delete(Guid id);
}