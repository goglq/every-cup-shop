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
    }

    public static void AddAppValidation(this IServiceCollection service)
    {
        service.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
    }

    public static void AddAppRepositories(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserEfRepository>();
    }

    public static void AddAppMapperProfiles(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(UserMappingProfile));
    }
}