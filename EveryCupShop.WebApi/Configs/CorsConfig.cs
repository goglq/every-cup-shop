namespace EveryCupShop.Configs;

public record CorsConfig(IReadOnlyList<string> Origins, IReadOnlyList<string> Methods, IReadOnlyList<string> Headers);