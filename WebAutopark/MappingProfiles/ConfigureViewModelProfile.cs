using AutoMapper;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Models;

namespace WebAutopark.MappingProfiles
{
    public class ConfigureViewModelProfile : Profile
    {
        public ConfigureViewModelProfile()
        {
            CreateMap<DetailModel, DetailViewModel>()
                .ReverseMap();
            CreateMap<VehicleModel, VehicleViewModel>()
                .ReverseMap();
            CreateMap<VehicleTypeModel, VehicleTypeViewModel>()
                .ReverseMap();
            CreateMap<OrderModel, OrderViewModel>()
                .ReverseMap();
        }
    }
}