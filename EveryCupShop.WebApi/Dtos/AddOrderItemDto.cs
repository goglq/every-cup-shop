namespace EveryCupShop.Dtos;

public record AddOrderItemDto(Guid CupId, Guid OrderId, int Amount);