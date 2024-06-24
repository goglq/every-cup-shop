using AutoMapper;
using EveryCupShop.Core.Models;
using EveryCupShop.ViewModels;

namespace EveryCupShop.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserViewModel>();
        CreateMap<User, CreateUserViewModel>();
        CreateMap<User, ChangeUserEmailViewModel>();
        CreateMap<User, ChangeUserPasswordViewModel>();
        
        CreateMap<bool, CheckEmailViewModel>()
            .ForMember(dest => dest.IsEmailAvailable, opt => opt.MapFrom(src => src));
        CreateMap<(string accessToken, string refreshToken), TokensViewModel>()
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.refreshToken));
    }
}