using EveryCupShop.Dtos;
using FluentValidation;

namespace EveryCupShop.Validators;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(dto => dto.CupIdsAmount)
            .NotEmpty()
            .WithMessage("Order Items are not provided");
    }
}