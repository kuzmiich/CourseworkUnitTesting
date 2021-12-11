using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.BusinessLayer.Services.Base;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Enums;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.BusinessLayer.Services
{
    public class VehicleService : Service<VehicleModel, Vehicle, IRepository<Vehicle>>, IVehicleService
    {
        public VehicleService(IRepository<Vehicle> repository, IMapper mapper) : base(repository, mapper)
        {
        }
        private static IEnumerable<Vehicle> GetOrderedVehicleBySortString(IEnumerable<Vehicle> vehicles, VehicleSortCriteria sortCriteria) 
            => sortCriteria switch
        {
            VehicleSortCriteria.Name => vehicles.OrderBy(v => v.ModelName),
            VehicleSortCriteria.Type => vehicles.OrderBy(v => v.Type),
            VehicleSortCriteria.Mileage => vehicles.OrderBy(v => v.Mileage),
            _ => vehicles.OrderBy(v => v.Id),
        };

        public async Task<List<VehicleModel>> GetAll(VehicleSortCriteria sortCriteria, bool isAscending = true)
        {
            var orderedVehicles = new List<VehicleModel>();
            var vehicles = await Repository.GetAll().ToListAsync();
            if (isAscending)
            {
                orderedVehicles = GetOrderedVehicleBySortString(vehicles, sortCriteria) as List<VehicleModel>;
            }
            else
            {
                orderedVehicles = vehicles.OrderByDescending(v => v) as List<VehicleModel>;
            }
            return orderedVehicles;
        }

        public override async Task<List<VehicleModel>> GetAll() => await GetAll(VehicleSortCriteria.Id);

    }
}