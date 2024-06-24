using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace EveryCupShop.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IUserRepository userRepository, 
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
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

    public async Task<User> Change(User user)
    {
        user.Password = _passwordHasher.HashPassword(user, user.Password);

        await _userRepository.Update(user);
        await _userRepository.Save();

        return user;
    }

    public async Task<User> ChangeEmail(Guid id, string email)
    {
        var user = await _userRepository.Find(id);

        if (user is null)
            throw new UserNotFoundException();

        user.Email = email;

        await _userRepository.Update(user);
        await _userRepository.Save();

        return user;
    }

    public async Task ChangePassword(Guid id, string password)
    {
        var user = await _userRepository.Find(id);

        if (user is null)
            throw new UserNotFoundException();

        user.Password = _passwordHasher.HashPassword(user, password);
    }

    public async Task<User> Delete(Guid id)
    {
        var user = await _userRepository.Find(id);

        if (user is null)
            throw new UserNotFoundException();

        await _userRepository.Delete(user);
        await _userRepository.Save();

        return user;
    }
}