using EveryCupShop.Core.Enums;
using EveryCupShop.Core.Interfaces;

namespace EveryCupShop.Core.Models;

public class Order : IEntity
{
    public Guid Id { get; init; }
    
    public User User { get; set; }
    
    public Guid UserId { get; set; }
    
    public DateTime Created { get; set; }
    
    public DateTime? Updated { get; set; }
    
    public IList<OrderItem> OrderItems { get; set; }
    
    public OrderState State { get; set; }
}