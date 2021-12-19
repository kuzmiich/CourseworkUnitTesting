using System.Security.Claims;
using AutoMapper;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Entities.Identity;
using WebAutopark.Models;

namespace WebAutopark.MappingProfiles
{
    public class ConfigureViewModelProfile : Profile
    {
        public ConfigureViewModelProfile()
        {
            CreateMap<User, ClaimsPrincipal>()
                .ReverseMap();
            CreateMap<ProductModel, ProductViewModel>()
                .ReverseMap();
            CreateMap<ShoppingCartItemModel, ShoppingCartItemViewModel>()
                .ReverseMap();
            CreateMap<VehicleTypeModel, VehicleTypeViewModel>()
                .ReverseMap();
            CreateMap<OrderModel, OrderViewModel>()
                .ReverseMap();
        }
    }
}