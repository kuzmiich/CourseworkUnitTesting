using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAutopark.BusinessLayer.Interfaces
{
    public interface IService<T> : IDisposable, IAsyncDisposable
        where T : class
    {
        Task<List<T>> GetAll();
        
        Task<T> GetById(Guid id);
        
        Task<T> Create(T entity);

        Task<T> Update(T model);

        Task Delete(Guid id);
    }
}