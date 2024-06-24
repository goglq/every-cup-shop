using EveryCupShop.Dtos;
using FluentValidation;

namespace EveryCupShop.Validators;

public class CreateCupAttachmentDtoValidator : AbstractValidator<CreateCupAttachmentDto>
{
    public CreateCupAttachmentDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(dto => dto.Price)
            .NotEmpty()
            .WithMessage("Price is required")
            .GreaterThan(0)
            .WithMessage("Price is negative");
        
        RuleFor(dto => dto.Amount)
            .NotEmpty()
            .WithMessage("Amount is required")
            .GreaterThan(0)
            .WithMessage("Amount is negative");
    }
}