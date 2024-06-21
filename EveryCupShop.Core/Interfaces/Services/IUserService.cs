using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface IUserService
{
    Task<User> CreateUser(string email, string password);

    Task<IList<User>> GetUsers();
    
    Task<User> GetUser(string email);
    
    Task<User> GetUser(Guid id);

    Task<bool> CheckEmail(string email);

    Task<(string, string)> SignIn(string email, string password);

    Task SignUp(string email, string password);
}