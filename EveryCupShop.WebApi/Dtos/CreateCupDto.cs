namespace EveryCupShop.Dtos;

public record CreateCupDto(string Name, string Description, decimal Price, int Amount);