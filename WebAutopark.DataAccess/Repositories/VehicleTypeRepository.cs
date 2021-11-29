using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Repositories
{
    public class VehicleTypeRepository : BaseRepository<VehicleType>
    {
        public VehicleTypeRepository(WebAutoparkContext context) : base(context)
        {
        }
    }
}