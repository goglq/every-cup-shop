using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    private readonly ITokenService _tokenService;

    private readonly ITokenRepository _tokenRepository;

    public UserService(ITokenService tokenService, IUserRepository userRepository, ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _tokenService = tokenService;
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

    public async Task<(string accessToken, string refreshToken)> SignIn(string email, string password)
    {
        var user = await _userRepository.Find(email);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var tokens = _tokenService.GenerateTokens(user);

        var token = await _tokenRepository.FindByUserId(user.Id);

        if (token is null)
        {
            await _tokenRepository.Add(new Token()
            {
                RefreshToken = tokens.refreshToken,
                UserId = user.Id
            });
        }
        else
        {
            token.RefreshToken = tokens.refreshToken;
            _tokenRepository.Update(token);
        }

        await _tokenRepository.Save();

        return tokens;
    }

    public async Task SignUp(string email, string password)
    {
        var candidate = await _userRepository.Find(email);

        if (candidate is not null)
        {
            throw new EmailIsTakenException();
        }

        var newUser = new User
        {
            Email = email,
            Password = password
        };

        await _userRepository.Add(newUser);

        await _userRepository.Save();
    }
}