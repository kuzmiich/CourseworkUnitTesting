using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Repositories
{
    public class VehicleTypeRepository : ConnectionRepository, IRepository<VehicleType>
    {
        private const string QueryGetAll = "SELECT * FROM VehicleTypes " +
                                           "ORDER BY VehicleTypeId";

        private const string QueryGetById = "SELECT * FROM VehicleTypes WHERE VehicleTypeId = @id";

        private const string QueryCreate = "INSERT INTO VehicleTypes (TypeName, TaxCoefficient) VALUES(@TypeName, @TaxCoefficient)";

        private const string QueryUpdate = "UPDATE VehicleTypes SET TypeName = @TypeName, TaxCoefficient = @TaxCoefficient WHERE VehicleTypeId = @VehicleTypeId";

        private const string QueryDelete = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @id";

        public VehicleTypeRepository(DbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<VehicleType> GetById(int id) => await DbConnection.QueryFirstAsync<VehicleType>(QueryGetById, new { id });

        public async Task<IEnumerable<VehicleType>> GetAll() => await DbConnection.QueryAsync<VehicleType>(QueryGetAll);

        public async Task Create(VehicleType element) => await DbConnection.ExecuteAsync(QueryCreate, element);

        public async Task Update(VehicleType element) => await DbConnection.ExecuteAsync(QueryUpdate, element);

        public async Task Delete(int id) => await DbConnection.ExecuteAsync(QueryDelete, new { id });
    }
}
