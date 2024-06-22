using EveryCupShop.Core.Interfaces;

namespace EveryCupShop.Core.Models;

public class Order : IEntity
{
    public Guid Id { get; init; }
    
    public User User { get; set; }
    
    public Guid UserId { get; set; }
    
    public IList<OrderItem> OrderItems { get; set; }
}