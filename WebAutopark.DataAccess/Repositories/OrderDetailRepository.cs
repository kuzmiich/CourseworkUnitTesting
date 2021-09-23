using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Repositories
{
    public class OrderDetailRepository : ConnectionRepository, IRepository<OrderDetail>
    {
        private const string QueryGetAll = "SELECT OrderDetails.*, Details.* "
                                           + "FROM OrderDetails INNER JOIN Details "
                                           + "ON OrderDetails.DetailId = Details.DetailId "
                                           + "ORDER BY OrderDetailId";

        private const string QueryGetById = "SELECT * FROM OrderDetails WHERE OrderDetailId = @id";

        private const string QueryCreate = "INSERT INTO OrderDetails (OrderId, DetailId, DetailAmount) "
                                           + "VALUES (@OrderId, @DetailId, @DetailAmount)";

        private const string QueryUpdate = "UPDATE OrderDetails SET OrderId = @OrderId, DetailId = @DetailId, DetailAmount = @DetailAmount "
                                           + "WHERE OrderDetailId = @id";

        private const string QueryDelete = "DELETE FROM OrderDetails WHERE OrdeDetailId = @id";

        public OrderDetailRepository(DbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<IEnumerable<OrderDetail>> GetAll() =>
            await DbConnection.QueryAsync<OrderDetail, Detail, OrderDetail>
            (
                QueryGetAll, (orderElement, detail) =>
                {
                    orderElement.Detail = detail;
                    return orderElement;
                },
                splitOn: "OrderDetailId"
            );

        public async Task<OrderDetail> GetById(int id)
        {
            var collection = await DbConnection.QueryAsync<OrderDetail, Detail, OrderDetail>
            (
                QueryGetById, (orderElement, detail) =>
                {
                    orderElement.Detail = detail;
                    return orderElement;
                },
                splitOn: "OrderDetailId",
                param: new { id }
            );

            return collection.First();
        }
        public async Task Create(OrderDetail element) => await DbConnection.ExecuteAsync(QueryCreate, element);

        public async Task Update(OrderDetail element) => await DbConnection.ExecuteAsync(QueryUpdate, element);

        public async Task Delete(int id) => await DbConnection.ExecuteAsync(QueryDelete, new { id });
    }
}
