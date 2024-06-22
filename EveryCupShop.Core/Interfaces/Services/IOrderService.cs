using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CreateOrder(Guid userId);
}