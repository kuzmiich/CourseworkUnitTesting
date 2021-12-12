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

        private static IOrderedEnumerable<Vehicle> GetOrderedVehicleBySortString(IEnumerable<Vehicle> vehicles,
            VehicleSortCriteria sortCriteria,
            bool isAscending = true)
            => sortCriteria switch
            {
                VehicleSortCriteria.Name => isAscending
                    ? vehicles.OrderBy(v => v.ModelName)
                    : vehicles.OrderByDescending(v => v.ModelName),
                VehicleSortCriteria.Type => isAscending
                    ? vehicles.OrderBy(v => v.Type)
                    : vehicles.OrderByDescending(v => v.Type),
                VehicleSortCriteria.Mileage => isAscending
                    ? vehicles.OrderBy(v => v.Mileage)
                    : vehicles.OrderByDescending(v => v.Mileage),
                VehicleSortCriteria.Id => isAscending
                    ? vehicles.OrderBy(v => v.Id)
                    : vehicles.OrderByDescending(v => v.Id),
                _ => isAscending
                    ? vehicles.OrderBy(v => v.Id)
                    : vehicles.OrderByDescending(v => v.Id)
            };

        public async Task<List<VehicleModel>> GetAll(VehicleSortCriteria sortCriteria, bool isAscending = true)
        {
            var vehicles = await Repository.GetAll().ToListAsync();

            return GetOrderedVehicleBySortString(vehicles, sortCriteria, isAscending) as List<VehicleModel>;
        }

        public override async Task<List<VehicleModel>> GetAll() => await GetAll(VehicleSortCriteria.Id);
    }
}