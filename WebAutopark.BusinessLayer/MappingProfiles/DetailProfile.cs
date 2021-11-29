using AutoMapper;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Entities;

namespace WebAutopark.BusinessLayer.MappingProfiles
{
    public class DetailProfile : Profile
    {
        public DetailProfile()
        {
            CreateMap<Detail, DetailModel>()
                .ReverseMap();
        }
    }
}