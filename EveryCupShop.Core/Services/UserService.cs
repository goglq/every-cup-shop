using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUser(string email, string password)
    {
        try
        {
            var user = await _userRepository.Add(new User()
            {
                Email = email,
                Password = password
            });

            await _userRepository.Save();

            return user;
        }
        catch (Exception e)
        {
            throw new EfException("User addition error. Email has unique constraint", e);
        }
    }

    public async Task<IList<User>> GetUsers()
    {
        var users = await _userRepository.GetAll();

        return users;
    }

    public async Task<User> GetUser(string email)
    {
        var user = await _userRepository.Get(email);

        return user;
    }
    
    public async Task<User> GetUser(Guid id)
    {
        var user = await _userRepository.Get(id);

        return user;
    }

    public async Task<bool> CheckEmail(string email)
    {
        var user = await _userRepository.Find(email);

        return user is not null;
    }
}