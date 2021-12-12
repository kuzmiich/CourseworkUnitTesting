using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAutopark.Core.Exceptions;
using static System.GC;

namespace WebAutopark.DataAccess.Repositories.Base
{
    /// <summary>
    /// Base Repository
    /// </summary>
    /// <typeparam name="TEntity">Repository entity</typeparam>
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected static bool _disposed = false;
        private readonly WebAutoparkContext _context;
        protected readonly DbSet<TEntity> Set;

        protected BaseRepository(WebAutoparkContext context)
        {
            _context = context;
            Set = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
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

            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public virtual async Task Delete(int id)
        {
            var deletedEntity = await Set.FindAsync(id);

            RepositoryException.IsEntityExists(deletedEntity, typeof(TEntity).FullName);

            Set.Remove(deletedEntity);
        }

        public Task Save() => _context.SaveChangesAsync();

        #region Dispose Repository

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _context.Dispose();
            }

            _disposed = true;
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        #endregion
    }
}