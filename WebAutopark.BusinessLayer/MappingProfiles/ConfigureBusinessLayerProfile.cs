using AutoMapper;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Entities;

namespace WebAutopark.BusinessLayer.MappingProfiles
{
    public class ConfigureBusinessLayerProfile : Profile
    {
        public ConfigureBusinessLayerProfile()
        {
            CreateMap<Product, ProductModel>()
                .ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemModel>()
                .ReverseMap();
            CreateMap<Order, OrderModel>()
                .ReverseMap();
            CreateMap<VehicleType, VehicleTypeModel>()
                .ReverseMap();
        }
    }
}