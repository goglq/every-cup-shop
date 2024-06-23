namespace EveryCupShop.Dtos;

public record CreateOrderDto(IDictionary<Guid, int> CupIdsAmount);