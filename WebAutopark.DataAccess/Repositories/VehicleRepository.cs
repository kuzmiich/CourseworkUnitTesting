using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>
    {
        public VehicleRepository(WebAutoparkContext context) : base(context)
        {
        }
    }
}