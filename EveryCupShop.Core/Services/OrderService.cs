using EveryCupShop.Core.Enums;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly IOrderItemRepository _orderItemRepository;

    private readonly IUserRepository _userRepository;

    public OrderService(
        IOrderRepository orderRepository, 
        IOrderItemRepository orderItemRepository, 
        IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _userRepository = userRepository;
    }

    public async Task<Order> CreateOrder(Guid userId, IDictionary<Guid, int> cupIds)
    {
        var user = await _userRepository.Find(userId);

        if (user is null)
            throw new UserNotFoundException();
        
        var newOrderItems = cupIds
            .Select(pair => new OrderItem
            {
                CupId = pair.Key,
                Amount = pair.Value,
            })
            .ToList();

        var newOrder = new Order
        {
            UserId = user.Id,
            OrderItems = newOrderItems,
            Created = DateTime.UtcNow,
            State = OrderState.Pending
        };

        var createdOrder = await _orderRepository.Add(newOrder);
        await _orderRepository.Save();

        return createdOrder;
    }

    public async Task<Order> ChangeOrderState(Guid orderId, OrderState state)
    {
        var order = await _orderRepository.Find(orderId);

        if (order is null)
            throw new DataNotFoundException();

        if (order.State == state)
            return order;

        order.State = state;
        var updatedOrder = await _orderRepository.Update(order);
        await _orderRepository.Save();
        return updatedOrder;
    }

    public async Task<Order> DeleteOrderItem(Guid orderId)
    {
        var order = await _orderRepository.Find(orderId);

        if (order is null)
            throw new DataNotFoundException();

        await _orderRepository.Delete(order);
        await _orderRepository.Save();

        return order;
    }

    public async Task<OrderItem> CreateOrderItem(Guid orderId, Guid cupId, int amount)
    {
        var order = await _orderRepository.Find(orderId);

        if (order is null)
            throw new DataNotFoundException();
        
        var newOrderItem = new OrderItem
        {
            OrderId = order.Id,
            CupId = cupId,
            Amount = amount
        };

        var createdOrderItem = await _orderItemRepository.Add(newOrderItem);
        await _orderItemRepository.Save();
        
        return createdOrderItem;
    }

    public async Task<OrderItem> SetOderItemAmount(Guid orderItemId, int amount)
    {
        var orderItem = await _orderItemRepository.Find(orderItemId);

        if (orderItem is null)
            throw new DataNotFoundException();

        orderItem.Amount = amount;

        var updatedOrderItem = await _orderItemRepository.Update(orderItem);
        await _orderItemRepository.Save();

        return updatedOrderItem;
    }

    public async Task<OrderItem> DeleteOrder(Guid orderItemId)
    {
        var orderItem = await _orderItemRepository.Find(orderItemId);

        if (orderItem is null)
            throw new DataNotFoundException();

        await _orderItemRepository.Delete(orderItem);
        await _orderItemRepository.Save();

        return orderItem;
    }
}