using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Repositories.ExtendRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> CreateInsert(int id);
    }
}
