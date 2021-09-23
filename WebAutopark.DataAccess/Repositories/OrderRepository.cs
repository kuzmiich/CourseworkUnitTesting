using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;
using WebAutopark.DataAccess.Repositories.ExtendRepository;

namespace WebAutopark.DataAccess.Repositories
{
    public class OrderRepository : ConnectionRepository, IOrderRepository
    {
        private const string QueryGetAll = "SELECT "
                                           + "O.*, V.VehicleId AS VId, V.VehicleTypeId, V.ModelName, V.RegistrationNumber, V.Weight, V.ManufactureYear, "
                                           + "V.Mileage, V.Color, V.EngineConsumption, V.TankCapacity "
                                           + "FROM Orders AS O "
                                           + "LEFT JOIN Vehicles AS V ON O.VehicleId = V.VehicleId ";

        private const string QueryGetById = "SELECT O.*, V.VehicleId AS VId, V.VehicleTypeId, V.ModelName, V.RegistrationNumber, " +
                                            "V.Weight, V.ManufactureYear, V.Mileage, V.Color, V.EngineConsumption, V.TankCapacity, " +
                                            "OD.OrderDetailId AS ODId, OD.OrderId, OD.DetailId, OD.DetailAmount, " + 
                                            "D.DetailId AS DId, D.Name " +
                                            "FROM Orders AS O " +
                                            "LEFT JOIN OrderDetails AS OD ON O.OrderId = OD.OrderId " +
                                            "JOIN Vehicles AS V ON O.VehicleId = V.VehicleId " + 
                                            "LEFT JOIN Details AS D ON OD.DetailId = D.DetailId " +
                                            "WHERE O.OrderId = @Id";

        private const string QueryCreate = "INSERT INTO Orders (VehicleId) VALUES(@VehicleId)";

        private const string QueryCreateInsert = "INSERT Orders (VehicleId) OUTPUT Inserted.OrderId, " +
                                                 "Inserted.VehicleId VALUES(@id)";

        private const string QueryUpdate = "UPDATE Orders SET VehicleId = @VehicleId WHERE OrderId = @id";

        private const string QueryDelete = "DELETE FROM Orders WHERE OrderId = @id";


        public OrderRepository(DbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await DbConnection.QueryAsync<Order, Vehicle, Order>
            (QueryGetAll, (order, vehicle) =>
                {
                    order.Vehicle = vehicle;
                    return order;
                },
                splitOn: "VId"
            );
        }

        public async Task<Order> Get(int id)
        {
            var orderDetails = new List<OrderDetail>();

            var collection = await DbConnection.QueryAsync<Order, Vehicle, OrderDetail, Detail, Order>
            (QueryGetById, (order1, vehicle, orderDetail, detail) =>
                {
                    order1.Vehicle = vehicle;
                    orderDetail.Detail = detail;
                    orderDetails.Add(orderDetail);
                    return order1;
                },
                splitOn: "VId, ODId, DId",
                param: new { id }
            );
            var order = collection.First();
            order.OrderDetails = orderDetails;
            return order;
        }

        public async Task<Order> CreateInsert(int id) => await DbConnection.QuerySingleAsync<Order>(QueryCreateInsert, new { id });

        public async Task Create(Order element) => await DbConnection.ExecuteAsync(QueryCreate, element);

        public async Task Update(Order element) => await DbConnection.ExecuteAsync(QueryUpdate, element);

        public async Task Delete(int id) => await DbConnection.ExecuteAsync(QueryDelete, new { id });
    }
}
