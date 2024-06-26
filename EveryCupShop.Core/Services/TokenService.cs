﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EveryCupShop.Core.Configs;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EveryCupShop.Core.Services;

public class TokenService : ITokenService
{
    private readonly JwtConfig _accessOptions;

    private readonly JwtConfig _refreshOptions;

    private readonly ITokenRepository _tokenRepository;

    private readonly IUserRepository _userRepository;

    public TokenService(IOptionsSnapshot<JwtConfig> options, ITokenRepository tokenRepository, IUserRepository userRepository)
    {
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
        _accessOptions = options.Get($"Access{nameof(JwtConfig)}");
        _refreshOptions = options.Get($"Refresh{nameof(JwtConfig)}");
    }
    
    public async Task<(string accessToken, string refreshToken)> RefreshTokens(string refreshToken)
    {
        var token = await _tokenRepository.Find(refreshToken);

        if (token is null) throw new ApiUnauthorizedException();

        var user = await _userRepository.Find(token.UserId, user => user.Roles);

        if (user is null) throw new UserNotFoundException();

        var tokens = GenerateTokens(user);

        token.RefreshToken = tokens.refreshToken;
        _tokenRepository.Update(token);
        await _tokenRepository.Save();

        return tokens;
    }

    public (string accessToken, string refreshToken) GenerateTokens(User user)
    {
        return (Generate(user, _accessOptions), Generate(user, _refreshOptions));
    }

    private string Generate(User user, JwtConfig config)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        var key = new SymmetricSecurityKey(config.SigningKeyBytes);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config.Issuer,
            audience: config.Audience,
            claims,
            expires: DateTime.Now.Add(config.Lifetime),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}