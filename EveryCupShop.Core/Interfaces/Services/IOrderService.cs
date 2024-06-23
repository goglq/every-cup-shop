using EveryCupShop.Core.Enums;
using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface IOrderService
{
    Task<Order> GetOrder(Guid id);

    Task<IList<Order>> GetOrders();

    Task<Order> CreateOrder(Guid userId, IDictionary<Guid, int> cupIds);

    Task<Order> ChangeOrderState(Guid orderId, OrderState state);
    
    Task<Order> DeleteOrder(Guid orderId);

    Task<OrderItem> GetOrderItem(Guid id);

    Task<IList<OrderItem>> GetOrderItems();
    
    Task<OrderItem> CreateOrderItem(Guid orderId, Guid cupId, int amount);

    Task<OrderItem> SetOderItemAmount(Guid orderItemId, int amount);

    Task<OrderItem> DeleteOrderItem(Guid orderItemId);
}