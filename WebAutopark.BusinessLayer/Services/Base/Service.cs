using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.BusinessLayer.Services.Base
{
    public abstract class Service<TEntity, TRepository> : IService<TEntity>
        where TEntity : class
        where TRepository : IRepository<TEntity>
    {
        protected readonly TRepository _repository;

        protected Service(TRepository repository)
        {
            _repository = repository;
        }
        
        public virtual Task<List<TEntity>> GetAll()
        {
            return _repository.GetAll() as Task<List<TEntity>>;
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);

            return entity;
        }
        public virtual async Task<TEntity> Create(TEntity entity)
        {
            var createdEntity = await _repository.Create(entity);
            await _repository.Save();

            return createdEntity;
        }

        public virtual async Task<TEntity> Update(TEntity model)
        {
            var updatedEntity = _repository.Update(model);
            await _repository.Save();

            return updatedEntity;
        }
        public virtual async Task Delete(Guid id)
        {
            await _repository.Delete(id);
            await _repository.Save();
        }
        public void Dispose()
        {
            _repository?.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return _repository.DisposeAsync();
        }

    }
}