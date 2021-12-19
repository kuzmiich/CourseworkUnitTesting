using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebAutopark.BusinessLayer.Interfaces.Base;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.BusinessLayer.Services.Base
{
    public abstract class Service<TModel, TEntity, TRepository> : IService<TModel>
        where TModel : class
        where TEntity : class
        where TRepository : IRepository<TEntity>
    {
        protected readonly IMapper Mapper;
        protected readonly TRepository Repository;

        protected Service(TRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<List<TModel>> GetAll()
        {
            var entities = Repository.GetAll();

            return await entities.ProjectTo<TModel>(Mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<TModel> GetById(int id)
        {
            var entity = await Repository.GetById(id);

            return Mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> Create(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);

            var createdEntity = await Repository.Create(entity);
            await Repository.Save();

            return Mapper.Map<TModel>(createdEntity);
        }

        public virtual async Task<TModel> Update(TModel model)
        {
            var mappedEntity = Mapper.Map<TEntity>(model);
            var updatedEntity = Repository.Update(mappedEntity);

            await Repository.Save();

            return Mapper.Map<TModel>(updatedEntity);
        }

        public async Task Delete(int id)
        {
            await Repository.Delete(id);
            await Repository.Save();
        }

        public void Dispose() => Repository?.Dispose();

        public ValueTask DisposeAsync() => Repository.DisposeAsync();
    }
}