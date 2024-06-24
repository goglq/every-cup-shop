using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace EveryCupShop.Core.Services;

public class UserService : IUserService
{
    private readonly IUserCachingRepository _userCachingRepository;

    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IUserCachingRepository userCachingCachingRepository,
        IPasswordHasher<User> passwordHasher)
    {
        _userCachingRepository = userCachingCachingRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> CreateUser(string email, string password)
    {
        try
        {
            var user = await _userCachingRepository.Add(new User()
            {
                Email = email,
                Password = password
            });

            await _userCachingRepository.Save();

            return user;
        }
        catch (Exception e)
        {
            throw new EfException("User addition error. Email has unique constraint", e);
        }
    }

    public async Task<IList<User>> GetUsers()
    {
        var users = await _userCachingRepository.GetAll();

        return users;
    }

    public async Task<User> GetUser(string email)
    {
        var user = await _userCachingRepository.Get(email);

        return user;
    }
    
    public async Task<User> GetUser(Guid id)
    {
        var user = await _userCachingRepository.Get(id);

        return user;
    }

    public async Task<bool> CheckEmail(string email)
    {
        var user = await _userCachingRepository.Find(email);

        return user is not null;
    }

    public async Task<User> Change(User user)
    {
        user.Password = _passwordHasher.HashPassword(user, user.Password);

        await _userCachingRepository.Update(user);
        await _userCachingRepository.Save();

        return user;
    }

    public async Task<User> ChangeEmail(Guid id, string email)
    {
        var user = await _userCachingRepository.Find(id);

        if (user is null)
            throw new UserNotFoundException();

        user.Email = email;

        await _userCachingRepository.Update(user);
        await _userCachingRepository.Save();

        return user;
    }

    public async Task ChangePassword(Guid id, string password)
    {
        var user = await _userCachingRepository.Find(id);

        if (user is null)
            throw new UserNotFoundException();

        user.Password = _passwordHasher.HashPassword(user, password);
    }

    public async Task<User> Delete(Guid id)
    {
        var user = await _userCachingRepository.Find(id);

        if (user is null)
            throw new UserNotFoundException();

        await _userCachingRepository.Delete(user);
        await _userCachingRepository.Save();

        return user;
    }
}