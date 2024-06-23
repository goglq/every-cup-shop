namespace EveryCupShop.Dtos;

public record ChangeCupAttachmentDto(Guid Id, string Name, string Description, decimal Price, int Amount);