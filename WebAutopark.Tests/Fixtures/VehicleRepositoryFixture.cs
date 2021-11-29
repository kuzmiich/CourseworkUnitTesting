using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.Tests.Fixtures.Base;

namespace WebAutopark.Tests.Fixtures
{
    internal class VehicleRepositoryFixture : RepositoryFixture<VehicleRepository>
    {
        public Guid Id { get; set; }
        protected override VehicleRepository CreateRepository() => new (Connection);

        protected override async Task InitDatabase()
        {
            var vehicles = new List<Vehicle>
            {
                new () { ModelName = "Name" },
                new () { ModelName = "Name2" } 
            };
            
            await Connection.Vehicles.AddRangeAsync(vehicles);
            await Connection.SaveChangesAsync();
            
            Id = Connection.VehicleTypes.First().Id;
        }
    }
}