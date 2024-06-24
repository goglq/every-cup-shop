using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EveryCupShop.Core.Services;

public class AuthService : IAuthService
{
    private const string AccessTokenKey = "accessToken";

    private static readonly CookieOptions CookieOptions = new()
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
    };

    private readonly ITokenService _tokenService;

    private readonly IUserRepository _userRepository;

    private readonly ITokenRepository _tokenRepository;

    private readonly IRoleRepository _roleRepository;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(
        ITokenService tokenService, 
        IUserRepository userRepository, 
        ITokenRepository tokenRepository,
        IRoleRepository roleRepository,
        IHttpContextAccessor httpContextAccessor, 
        IPasswordHasher<User> passwordHasher)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _roleRepository = roleRepository;
        _httpContextAccessor = httpContextAccessor;
        _passwordHasher = passwordHasher;
    }


    public async Task<(string accessToken, string refreshToken)> SignIn(string email, string password)
    {
        var user = await _userRepository.Find(email, user => user.Roles);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

        if (verificationResult == PasswordVerificationResult.Failed)
            throw new ApiUnauthorizedException("Wrong password");
        
        var tokens = _tokenService.GenerateTokens(user);

        var token = await _tokenRepository.FindByUserId(user.Id);

        if (token is null)
        {
            await _tokenRepository.Add(new Token
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
        
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(AccessTokenKey, tokens.accessToken, CookieOptions);

        return tokens;
    }

    public async Task<User> SignUp(string email, string password)
    {
        var candidate = await _userRepository.Find(email);

        if (candidate is not null)
            throw new EmailIsTakenException();

        var userRole = await _roleRepository.Get("User");
        
        var newUser = new User
        {
            Email = email,
            Roles = new List<Role>
            {
                userRole
            }  
        };

        var hashedPassword = _passwordHasher.HashPassword(newUser, password);
        newUser.Password = hashedPassword;
        
        var signedUser = await _userRepository.Add(newUser);
        await _userRepository.Save();

        return signedUser;
    }

    public async Task SignOut(Guid userId)
    {
        var token = await _tokenRepository.FindByUserId(userId);

        if (token is null)
            throw new ApiUnauthorizedException();
        
        await _tokenRepository.Delete(token);
        await _tokenRepository.Save();
        
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(AccessTokenKey);
    }

    public async Task<(string accessToken, string refreshToken)> Refresh(string refreshToken)
    {
        var tokens = await _tokenService.RefreshTokens(refreshToken);

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(AccessTokenKey, tokens.accessToken, CookieOptions);
        
        return tokens;
    }
}