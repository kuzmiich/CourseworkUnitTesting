using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAutopark.BusinessLayer.Interfaces.Base
{
    public interface IService<TModel> : IDisposable, IAsyncDisposable
        where TModel : class
    {
        Task<List<TModel>> GetAll();
        
        Task<TModel> GetById(Guid id);
        
        Task<TModel> Create(TModel entity);

        Task<TModel> Update(TModel model);

        Task Delete(Guid id);
    }
}