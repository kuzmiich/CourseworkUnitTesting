using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Services.Base;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;

namespace WebAutopark.BusinessLayer.Services
{
    public class DetailService : Service<Detail, DetailRepository>, IDetailService
    {
        public DetailService(DetailRepository repository) 
            : base(repository)
        {
        }
    }
}