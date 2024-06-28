namespace EveryCupShop.Dtos;

public record ChangeCupDto(Guid CupId, string Name, string Description, decimal Price, int Amount);