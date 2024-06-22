using EveryCupShop.Core.Enums;
using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CreateOrder(Guid userId, IDictionary<Guid, int> cupIds);

    Task<Order> ChangeOrderState(Guid orderId, OrderState state);
    
    Task<Order> DeleteOrderItem(Guid orderId);

    Task<OrderItem> CreateOrderItem(Guid orderId, Guid cupId, int amount);

    Task<OrderItem> SetOderItemAmount(Guid orderItemId, int amount);

    Task<OrderItem> DeleteOrder(Guid orderItemId);
}