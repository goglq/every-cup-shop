using EveryCupShop.Core.Enums;

namespace EveryCupShop.ViewModels;

public class OrderViewModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime Created { get; set; }
    
    public DateTime? Updated { get; set; }
    
    public OrderState State { get; set; }
    
    public IList<OrderItemViewModel> OrderItems { get; set; }
}