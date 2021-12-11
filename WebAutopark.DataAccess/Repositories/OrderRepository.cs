using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(WebAutoparkContext context) : base(context)
        {
        }
    }
}