using AutoMapper;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Models;

namespace WebAutopark.MappingProfiles
{
    public class ConfigureViewModelProfile : Profile
    {
        public ConfigureViewModelProfile()
        {
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