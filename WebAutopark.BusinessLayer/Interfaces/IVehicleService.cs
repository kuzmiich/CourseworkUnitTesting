using System.Collections.Generic;
using System.Threading.Tasks;
using WebAutopark.BusinessLayer.Interfaces.Base;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Enums;

namespace WebAutopark.BusinessLayer.Interfaces
{
    public interface IVehicleService : IService<VehicleModel>
    {
        Task<List<VehicleModel>> GetAll(VehicleSortCriteria sortCriteria, bool isAscending);
    }
}