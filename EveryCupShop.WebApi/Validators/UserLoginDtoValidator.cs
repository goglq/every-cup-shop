using EveryCupShop.Dtos;
using FluentValidation;

namespace EveryCupShop.Validators;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email");
        
        RuleFor(dto => dto.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}