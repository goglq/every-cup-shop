using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Services;
using EveryCupShop.Dtos;
using EveryCupShop.Infrastructure.Repositories;
using EveryCupShop.MappingProfiles;
using EveryCupShop.Validators;
using FluentValidation;

namespace EveryCupShop.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddAppServices(this IServiceCollection service)
    {
        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<IUserService, UserService>();
        service.AddScoped<IAuthService, AuthService>();
    }

    public static void AddAppValidation(this IServiceCollection service)
    {
        service.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
        service.AddScoped<IValidator<UserSignInDto>, UserSignInDtoValidator>();
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