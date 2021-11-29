using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAutopark.BusinessLayer.Services.Base;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Enums;
using WebAutopark.DataAccess.Repositories;

namespace WebAutopark.BusinessLayer.Services
{
    public class VehicleService : Service<Vehicle, VehicleRepository>
    {
        public VehicleService(VehicleRepository repository) : base(repository)
        {
        }
        private static IOrderedEnumerable<Vehicle> GetOrderedVehicleBySortString(IEnumerable<Vehicle> vehicles, VehicleSortCriteria sortCriteria) 
            => sortCriteria switch
        {
            VehicleSortCriteria.Name => vehicles.OrderBy(v => v.ModelName),
            VehicleSortCriteria.Type => vehicles.OrderBy(v => v.Type),
            VehicleSortCriteria.Mileage => vehicles.OrderBy(v => v.Mileage),
            _ => vehicles.OrderBy(v => v.Id),
        };

        public async Task<IEnumerable<Vehicle>> GetAll(VehicleSortCriteria sortCriteria, bool isAscending = true)
        {
            var orderedVehicles = Enumerable.Empty<Vehicle>();
            var vehicles = await _repository.GetAll().ToListAsync();
            if (isAscending)
            {
                orderedVehicles = GetOrderedVehicleBySortString(vehicles, sortCriteria);
            }
            else
            {
                orderedVehicles = vehicles.OrderByDescending(v => v);
            }
            return orderedVehicles;
        }

        public async Task<IEnumerable<Vehicle>> GetAll() => await GetAll(VehicleSortCriteria.Id);

    }
}