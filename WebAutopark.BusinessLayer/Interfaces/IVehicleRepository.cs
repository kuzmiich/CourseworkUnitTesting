using System.Collections.Generic;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Enums;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.BusinessLayer.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetAll(VehicleSortCriteria sortCriteria, bool isAscending);
    }
}
