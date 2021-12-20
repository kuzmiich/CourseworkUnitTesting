using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAutopark.Core.Exceptions;

namespace WebAutopark.DataAccess.Repositories.Base
{
    /// <summary>
    /// Base Repository
    /// </summary>
    /// <typeparam name="TEntity">Repository entity</typeparam>
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private static bool _disposed = false;
        protected readonly WebAutoparkContext Сontext;
        protected readonly DbSet<TEntity> Set;

        protected BaseRepository(WebAutoparkContext сontext)
        {
            Сontext = сontext;
            Set = сontext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Set.AsNoTracking();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            RepositoryException.IsIdValid(id);

            var foundEntity = await Set.FindAsync(id);

            RepositoryException.IsEntityExists(foundEntity, typeof(TEntity).FullName);

            return foundEntity;
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            var addedEntity = (await Set.AddAsync(entity)).Entity;
            
            return addedEntity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            RepositoryException.IsEntityExists(entity, typeof(TEntity).FullName);

            Сontext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public virtual async Task Delete(int id)
        {
            var deletedEntity = await Set.FindAsync(id);

            RepositoryException.IsEntityExists(deletedEntity, typeof(TEntity).FullName);

            Set.Remove(deletedEntity);
        }

        public Task Save() => Сontext.SaveChangesAsync();

        #region Dispose Repository

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    Сontext.Dispose();
            }

            _disposed = true;
        }
        
        public void Dispose()
        {
            Сontext.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return Сontext.DisposeAsync();
        }

        #endregion
    }
}