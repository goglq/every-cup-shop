using EveryCupShop.Core.Interfaces;

namespace EveryCupShop.Core.Models;

public class OrderItem : IEntity
{
    public Guid Id { get; init; }
    
    public Order Order { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Cup Cup { get; set; }
    
    public Guid CupId { get; set; }

    public int Amount { get; set; } = 1;
}