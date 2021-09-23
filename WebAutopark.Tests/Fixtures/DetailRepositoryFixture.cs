using System.Data.Common;
using WebAutopark.DataAccess.Repositories;
using WebAutopark.Tests.Fixtures.Base;

namespace WebAutopark.Tests.Fixtures
{
    public class DetailRepositoryFixture : RepositoryFixture<DetailRepository>
    {
        public DetailRepositoryFixture(DbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override DetailRepository CreateRepository() => new (DbConnection);

        protected override void InitDatabase()
        {
            throw new System.NotImplementedException();
        }
    }
}