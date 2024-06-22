using EveryCupShop.Dtos;
using FluentValidation;

namespace EveryCupShop.Validators;

public class ChangeOrderStateDtoValidator : AbstractValidator<ChangeOrderStateDto>
{
    public ChangeOrderStateDtoValidator()
    {
        RuleFor(dto => dto.OrderId)
            .NotEmpty()
            .WithMessage("Order id is not provided");
        
        RuleFor(dto => dto.State)
            .NotEmpty()
            .WithMessage("New state is not provided");
    }
}