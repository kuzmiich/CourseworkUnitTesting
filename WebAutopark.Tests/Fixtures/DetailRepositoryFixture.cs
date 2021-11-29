using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.Tests.Fixtures.Base;

namespace WebAutopark.Tests.Fixtures
{
    internal class DetailRepositoryFixture : RepositoryFixture<DetailRepository>
    {
        public Guid Id { get; set; }
        protected override DetailRepository CreateRepository() => new (Connection);

        protected override async Task InitDatabase()
        {
            var details = new List<Detail>
            {
                new () { Name = "Name" },
                new () { Name = "Name2" } 
            };
            await Connection.Details.AddRangeAsync(details);
            await Connection.SaveChangesAsync();
            Id = Connection.Details.First().Id;
        }
    }
}