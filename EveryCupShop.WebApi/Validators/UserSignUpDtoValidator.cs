using EveryCupShop.Dtos;
using FluentValidation;

namespace EveryCupShop.Validators;

public class UserSignUpDtoValidator : AbstractValidator<UserSignUpDto>
{
    public UserSignUpDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(dto => dto.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}