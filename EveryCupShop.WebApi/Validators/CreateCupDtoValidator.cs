using EveryCupShop.Dtos;
using FluentValidation;

namespace EveryCupShop.Validators;

public class CreateCupDtoValidator : AbstractValidator<CreateCupDto>
{
    public CreateCupDtoValidator()
    {
        RuleFor(dto => dto.CupShapeId)
            .NotEmpty()
            .WithMessage("Cup shape id is required");
        
        RuleFor(dto => dto.CupAttachmentId)
            .NotEmpty()
            .WithMessage("Cup attachment id is required");
    }
}