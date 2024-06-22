using EveryCupShop.Core.Enums;

namespace EveryCupShop.Dtos;

public class ChangeOrderStateDto
{
    public Guid OrderId { get; init; }
    
    public OrderState State { get; init; }
}