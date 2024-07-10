using EveryCupShop.Dtos;
using FluentValidation;

namespace EveryCupShop.Validators;

public class CreateCupDtoValidator : AbstractValidator<CreateCupDto>
{
    public CreateCupDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        
        RuleFor(dto => dto.Price)
            .NotNull()
            .WithMessage("Price is required");
    }
}