using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.DataAccess.Repositories
{
    public class DetailRepository : BaseRepository<Detail>
    {
        public DetailRepository(WebAutoparkContext context) : base(context)
        {
        }
    }
}