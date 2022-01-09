using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.Tests.Fixtures.Base;

namespace WebAutopark.Tests.Fixtures
{
    internal class VehicleTypeRepositoryFixture : RepositoryFixture<VehicleTypeRepository>
    {
        protected override VehicleTypeRepository CreateRepository() => new (Connection);

        protected override async Task InitDatabase()
        {
            var entityEntry = await Connection.VehicleTypes.AddAsync(new VehicleType { TypeName = "Name" });
            Id = entityEntry.Entity.Id;
            
            await Connection.VehicleTypes.AddAsync(entityEntry.Entity);
            await Connection.SaveChangesAsync();
            entityEntry.State = EntityState.Detached;
        }
    }
}