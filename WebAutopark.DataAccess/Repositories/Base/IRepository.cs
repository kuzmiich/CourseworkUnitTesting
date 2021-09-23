using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAutopark.DataAccess.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();

        Task Create(T element);
        Task Update(T element);
        
        Task Delete(int id);
    }
}
