using EveryCupShop.Core.Enums;

namespace EveryCupShop.Dtos;

public record ChangeOrderStateDto(Guid OrderId, OrderState State);