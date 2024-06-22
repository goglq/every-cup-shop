using AutoMapper;
using EveryCupShop.Core.Models;
using EveryCupShop.ViewModels;

namespace EveryCupShop.MappingProfiles;

public class CupMappingProfile : Profile
{
    public CupMappingProfile()
    {
        CreateMap<Cup, CreateCupViewModel>();
        CreateMap<Cup, ChangeCupViewModel>();

        CreateMap<CupShape, CreateCupShapeViewModel>();
        
        CreateMap<CupAttachment, CreateCupAttachmentViewModel>();
    }
}