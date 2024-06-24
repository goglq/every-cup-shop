using EveryCupShop.Dtos;
using FluentValidation;

namespace EveryCupShop.Validators;

public class UserSignInDtoValidator : AbstractValidator<UserSignInDto>
{
    public UserSignInDtoValidator()
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