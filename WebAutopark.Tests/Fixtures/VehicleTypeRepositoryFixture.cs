using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.Tests.Fixtures.Base;

namespace WebAutopark.Tests.Fixtures
{
    internal class VehicleTypeRepositoryFixture : RepositoryFixture<VehicleTypeRepository>
    {
        public int Id { get; set; }
        protected override VehicleTypeRepository CreateRepository() => new (Connection);

        protected override async Task InitDatabase()
        {
            var vehicleTypes = new List<VehicleType>
            {
                new () { TypeName = "Name" },
                new () { TypeName = "Name2" } 
            };
            
            await Connection.VehicleTypes.AddRangeAsync(vehicleTypes);
            await Connection.SaveChangesAsync();
            
            Id = Connection.VehicleTypes.First().Id;
        }
    }
}