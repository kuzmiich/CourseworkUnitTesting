using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAutopark.DataAccess.Repositories.Base
{
    public interface IRepository<T> : IDisposable, IAsyncDisposable
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int id);

        Task<T> Create(T element);
        T Update(T element);

        Task Save();
        Task Delete(int id);
    }
}