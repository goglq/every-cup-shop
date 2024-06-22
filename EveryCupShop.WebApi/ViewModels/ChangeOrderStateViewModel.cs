using EveryCupShop.Core.Enums;

namespace EveryCupShop.ViewModels;

public class ChangeOrderStateViewModel
{
    public Guid Id { get; set; }
    
    public OrderState State { get; set; }
}