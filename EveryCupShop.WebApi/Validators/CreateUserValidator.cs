using EveryCupShop.Dtos;
using FluentValidation;

namespace EveryCupShop.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(user => user.Email).EmailAddress().WithMessage("Invalid email address");
        RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required");
    }
}