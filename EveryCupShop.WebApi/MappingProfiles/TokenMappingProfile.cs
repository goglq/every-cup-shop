using AutoMapper;
using EveryCupShop.ViewModels;

namespace EveryCupShop.MappingProfiles;

public class TokenMappingProfile : Profile
{
    public TokenMappingProfile()
    {
        CreateMap<(string accessToken, string refreshToken), TokensViewModel>();
    }
}