using AutoMapper;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.BusinessLayer.Services.Base;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.BusinessLayer.Services
{
    public class DetailService : Service<DetailModel, Detail, IRepository<Detail>>, IDetailService
    {
        public DetailService(IRepository<Detail> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}