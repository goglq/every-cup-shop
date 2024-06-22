namespace EveryCupShop.Dtos;

public class CreateOrderDto
{
    public IDictionary<Guid, int> CupIdsAmount { get; init; }
}