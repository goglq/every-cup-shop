using EveryCupShop.Configs;
using EveryCupShop.Core.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace EveryCupShop.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void SetupCors(this WebApplicationBuilder builder)
    {
        var corsConfig = builder.Configuration.GetSection(nameof(CorsConfig)).Get<CorsConfig>();

        if (corsConfig is null)
        {
            throw new Exception("Cors config isn't set");
        }
    
        builder.Services.AddCors(options => options.AddPolicy("Default", policyBuilder => policyBuilder
            .WithOrigins(corsConfig.Origins.ToArray())
            .WithHeaders(corsConfig.Headers.ToArray())
            .WithMethods(corsConfig.Methods.ToArray())
        ));
    }

    public static void SetupJwt(this WebApplicationBuilder builder)
    {
        const string accessJwtConfigKey = $"Access{nameof(JwtConfig)}";
        const string refreshJwtConfigKey = $"Refresh{nameof(JwtConfig)}";
    
        var accessJwtConfigSection = builder.Configuration.GetSection(accessJwtConfigKey);
        var accessJwtConfig = accessJwtConfigSection.Get<JwtConfig>();
    
        if (accessJwtConfig is null)
        {
            throw new Exception("Jwt authentication isn't set");
        }
    
        builder.Services.Configure<JwtConfig>(accessJwtConfigKey, builder.Configuration.GetSection(accessJwtConfigKey));
        builder.Services.Configure<JwtConfig>(refreshJwtConfigKey, builder.Configuration.GetSection(refreshJwtConfigKey));

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = accessJwtConfig.Issuer,
                ValidAudience = accessJwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(accessJwtConfig.SigningKeyBytes)
            };
        });
    }
}