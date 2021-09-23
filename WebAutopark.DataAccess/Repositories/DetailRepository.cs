using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Repositories
{
    public class DetailRepository : ConnectionRepository, IRepository<Detail>
    {
        private const string QueryGetAll = "SELECT * FROM Details " +
                                           "ORDER BY DetailId";

        private const string QueryGetById = "SELECT * FROM Details WHERE DetailId = @id";

        private const string QueryCreate = "INSERT INTO Details (Name) VALUES(@Name)";

        private const string QueryUpdate = "UPDATE Details SET Name = @Name WHERE DetailId = @DetailId";

        private const string QueryDelete = "DELETE FROM Details WHERE DetailId = @id";

        public DetailRepository(DbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<Detail> GetById(int id) => await DbConnection.QueryFirstAsync<Detail>(QueryGetById, new { id });

        public async Task<IEnumerable<Detail>> GetAll() => await DbConnection.QueryAsync<Detail>(QueryGetAll);

        public async Task Create(Detail element) => await DbConnection.ExecuteAsync(QueryCreate, element);

        public async Task Update(Detail element) => await DbConnection.ExecuteAsync(QueryUpdate, element);

        public async Task Delete(int id) => await DbConnection.ExecuteAsync(QueryDelete, new { id });
    }
}
