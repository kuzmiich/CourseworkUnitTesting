using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WebAutopark.BusinessLogic.Models;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.DataAccess.Repositories.Base;
using WebAutopark.DataAccess.Repositories.ExtendRepository;
using DetailViewModel = WebAutopark.Models.DetailViewModel;

namespace WebAutopark.Extensions
{
    public static class DIExtension
    {
        public static void AddEntityRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Detail>, DetailRepository>();

            services.AddScoped<IVehicleRepository, VehicleRepository>();

            services.AddScoped<IRepository<VehicleType>, VehicleTypeRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IRepository<OrderDetail>, OrderDetailRepository>();
        }

        public static void AddAutoMapperIntoAutopark(this IServiceCollection services)
        {
            services.AddAutoMapper(
                cfg =>
                {
                    cfg.CreateMap<VehicleType, VehicleTypeViewModel>()
                        .ForMember(dest => dest.VehicleTypeId, act => act.MapFrom(src => src.VehicleTypeId))
                        .ReverseMap();

                    cfg.CreateMap<Vehicle, VehicleViewModel>()
                        .ForMember(dest => dest.VehicleId, act => act.MapFrom(src => src.VehicleId))
                        .ReverseMap();

                    cfg.CreateMap<Detail, DetailViewModel>()
                        .ForMember(dest => dest.DetailId, act => act.MapFrom(src => src.DetailId))
                        .ReverseMap();

                    cfg.CreateMap<OrderDetail, OrderDetailViewModel>()
                        .ForMember(dest => dest.OrderDetailId, act => act.MapFrom(src => src.OrderDetailId))
                        .ReverseMap();

                    cfg.CreateMap<Order, OrderViewModel>()
                        .ForMember(dest => dest.OrderId, act => act.MapFrom(src => src.OrderId))
                        .ReverseMap();
                }
            );
        }
    }
}