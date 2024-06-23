using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;
using EveryCupShop.Core.Services;
using EveryCupShop.Dtos;
using EveryCupShop.Infrastructure.Repositories;
using EveryCupShop.MappingProfiles;
using EveryCupShop.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace EveryCupShop.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddAppServices(this IServiceCollection service)
    {
        service.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        
        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<IAuthService, AuthService>();
        
        service.AddScoped<IUserService, UserService>();

        service.AddScoped<IEmailSender, MimeEmailSender>();
    }

    public static void AddAppValidation(this IServiceCollection service)
    {
        service.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
        service.AddScoped<IValidator<UserSignInDto>, UserSignInDtoValidator>();
        service.AddScoped<IValidator<UserSignUpDto>, UserSignUpDtoValidator>();
    }

    public static void AddAppRepositories(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserEfRepository>();
        service.AddScoped<ITokenRepository, TokenEfRepository>();
    }

    public static void AddAppMapperProfiles(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(UserMappingProfile));
        service.AddAutoMapper(typeof(TokenMappingProfile));
    }
}