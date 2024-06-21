using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EveryCupShop.Core.Configs;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace EveryCupShop.Core.Services;

public class TokenService : ITokenService
{
    private readonly JwtConfig _accessOptions;

    private readonly JwtConfig _refreshOptions;

    public TokenService(IOptionsSnapshot<JwtConfig> options)
    {
        _accessOptions = options.Get($"Access{nameof(JwtConfig)}");
        _refreshOptions = options.Get($"Refresh{nameof(JwtConfig)}");
    }
    
    public async Task<(string accessToken, string refreshToken)> RefreshTokens(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public (string accessToken, string refreshToken) GenerateTokens(User user)
    {
        return (Generate(user, _accessOptions), Generate(user, _refreshOptions));
    }

    private string Generate(User user, JwtConfig config)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

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