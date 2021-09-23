using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Enums;
using WebAutopark.DataAccess.Repositories.Base;
using WebAutopark.DataAccess.Repositories.ExtendRepository;

namespace WebAutopark.DataAccess.Repositories
{
    public class VehicleRepository : ConnectionRepository, IVehicleRepository
    {
        private const string QueryGetAll = "SELECT V.*, VT.VehicleTypeId AS VTId, VT.TypeName, VT.TaxCoefficient " + 
                                           "FROM Vehicles AS V " +
                                           "INNER JOIN VehicleTypes AS VT ON V.VehicleTypeId = VT.VehicleTypeId";

        private const string QueryGetById = "SELECT V.*, VT.VehicleTypeId AS VTId, VT.TypeName, VT.TaxCoefficient " + 
                                            "FROM Vehicles AS V " +
                                            "INNER JOIN VehicleTypes AS VT ON V.VehicleTypeId = VT.VehicleTypeId " +
                                            "WHERE V.VehicleId = @id";

        private const string QueryCreate = "INSERT INTO Vehicles " +
                                           "(VehicleTypeId, ModelName, RegistrationNumber, Weight, ManufactureYear, Mileage, Color, EngineConsumption, TankCapacity) " +
                                           "VALUES" +
                                           "(@VehicleTypeId, @ModelName, @RegistrationNumber, @Weight, @ManufactureYear, @Mileage, @Color, @EngineConsumption, @TankCapacity)";


        private const string QueryUpdate = "UPDATE Vehicles SET " +
                                           "VehicleTypeId = @VehicleTypeId, " +
                                           "ModelName = @ModelName, " +
                                           "RegistrationNumber = @RegistrationNumber, " +
                                           "Weight = @Weight, " +
                                           "ManufactureYear = @ManufactureYear, " +
                                           "Mileage = @Mileage, " +
                                           "Color = @Color, " +
                                           "EngineConsumption = @EngineConsumption, " +
                                           "TankCapacity = @TankCapacity " +
                                           "WHERE VehicleId = @VehicleId";

        private const string QueryDelete = "DELETE FROM Vehicles WHERE VehicleId = @id";

        public VehicleRepository(DbConnection dbConnection) : base(dbConnection)
        {
        }

        private static string GetVehicleSortString(VehicleSortCriteria sortCriteria) => sortCriteria switch
            {
                VehicleSortCriteria.Name => "V.ModelName",
                VehicleSortCriteria.Type => "VT.TypeName",
                VehicleSortCriteria.Mileage => "V.Mileage",
                _ => "V.VehicleId",
            };

        public async Task<IEnumerable<Vehicle>> GetAll(VehicleSortCriteria sortCriteria, bool isAscending = true)
        {
            var getAllSorted = isAscending ? $"{QueryGetAll} ORDER BY {GetVehicleSortString(sortCriteria)} ASC" : 
                $"{QueryGetAll} ORDER BY {GetVehicleSortString(sortCriteria)} DESC";

            return await DbConnection.QueryAsync<Vehicle, VehicleType, Vehicle>(getAllSorted,
                (vehicle, vehicleType) =>
                {
                    vehicle.VehicleType = vehicleType;
                    return vehicle;
                },
                splitOn: "VTId");
        }

        public async Task<IEnumerable<Vehicle>> GetAll() => await GetAll(VehicleSortCriteria.Id);

        public async Task<Vehicle> GetById(int id)
        {
            var collection = await DbConnection.QueryAsync<Vehicle, VehicleType, Vehicle>(QueryGetById,
                (vehicle, vehicleType) =>
                {
                    vehicle.VehicleType = vehicleType;
                    return vehicle;
                },
                splitOn: "VTId",
                param: new { id }
            );

            return collection.First();
        }
            

        public async Task Create(Vehicle element) => await DbConnection.ExecuteAsync(QueryCreate, element);

        public async Task Update(Vehicle element) => await DbConnection.ExecuteAsync(QueryUpdate, element);

        public async Task Delete(int id) => await DbConnection.ExecuteAsync(QueryDelete, new { id });
    }
}
