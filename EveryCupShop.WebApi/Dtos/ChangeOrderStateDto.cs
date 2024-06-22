using EveryCupShop.Core.Enums;

namespace EveryCupShop.Dtos;

public class ChangeOrderStateDto
{
    public Guid OrderId { get; set; }
    
    public OrderState State { get; set; }
}