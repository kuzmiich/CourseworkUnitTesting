using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Entities.Base;
using WebAutopark.Core.Exceptions;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(WebAutoparkContext сontext) : base(сontext)
        {
        }

        public override async Task<Order> Create(Order entity)
        {
            Set.Attach(entity);

            return await base.Create(entity);
        }

        public override async Task Delete(int id)
        {
            var deletedEntity = await Set.FindAsync(id);

            RepositoryException.IsEntityExists(deletedEntity, typeof(Order).FullName);

            Set.Attach(deletedEntity);
            
            Set.Remove(deletedEntity);
        }
    }
}