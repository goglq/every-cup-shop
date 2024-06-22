using AutoMapper;
using EveryCupShop.Core.Models;
using EveryCupShop.ViewModels;

namespace EveryCupShop.MappingProfiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, CreateOrderViewModel>();
        CreateMap<Order, ChangeOrderStateViewModel>();
        CreateMap<Order, AddOrderItemViewModel>();
    }
}