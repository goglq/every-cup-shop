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
        
        service.AddScoped<IOrderService, OrderService>();
        service.AddScoped<ICupService, CupService>();

        service.AddScoped<IEmailSender, MimeEmailSender>();
    }

    public static void AddAppValidation(this IServiceCollection service)
    {
        service.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
        service.AddScoped<IValidator<UserSignInDto>, UserSignInDtoValidator>();
        service.AddScoped<IValidator<UserSignUpDto>, UserSignUpDtoValidator>();
        
        service.AddScoped<IValidator<CreateOrderDto>, CreateOrderDtoValidator>();
        service.AddScoped<IValidator<ChangeOrderStateDto>, ChangeOrderStateDtoValidator>();

        service.AddScoped<IValidator<CreateCupShapeDto>, CreateCupShapeDtoValidator>();
        service.AddScoped<IValidator<CreateCupAttachmentDto>, CreateCupAttachmentDtoValidator>();
        service.AddScoped<IValidator<CreateCupDto>, CreateCupDtoValidator>();
    }

    public static void AddAppRepositories(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserEfRepository>();
        service.AddScoped<ITokenRepository, TokenEfRepository>();
        
        service.AddScoped<ICupRepository, CupEfRepository>();
        service.AddScoped<ICupShapeRepository, CupShapeEfRepository>();
        service.AddScoped<ICupAttachmentRepository, CupAttachmentEfRepository>();
        
        service.AddScoped<IOrderRepository, OrderEfRepository>();
        service.AddScoped<IOrderItemRepository, OrderItemEfRepository>();
    }

    public static void AddAppMapperProfiles(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(UserMappingProfile));
        service.AddAutoMapper(typeof(TokenMappingProfile));
        service.AddAutoMapper(typeof(OrderMappingProfile));
        service.AddAutoMapper(typeof(CupMappingProfile));
    }
}