using AutoMapper;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Entities;

namespace WebAutopark.BusinessLayer.MappingProfiles
{
    public class ConfigureBusinessLayerProfile : Profile
    {
        public ConfigureBusinessLayerProfile()
        {
            CreateMap<Detail, DetailModel>()
                .ReverseMap();
            CreateMap<Order, OrderModel>()
                .ReverseMap();
            CreateMap<Vehicle, VehicleModel>()
                .ReverseMap();
            CreateMap<VehicleType, VehicleTypeModel>()
                .ReverseMap();
        }
    }
}