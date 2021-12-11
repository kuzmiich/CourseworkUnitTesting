using AutoMapper;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.BusinessLayer.Services.Base;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.BusinessLayer.Services
{
    public class VehicleTypeService : Service<VehicleTypeModel, VehicleType, IRepository<VehicleType>>, IVehicleTypeService
    {
        public VehicleTypeService(IRepository<VehicleType> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}