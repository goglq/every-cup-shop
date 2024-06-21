using EveryCupShop.Dtos;
using FluentValidation;

namespace EveryCupShop.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email address");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}