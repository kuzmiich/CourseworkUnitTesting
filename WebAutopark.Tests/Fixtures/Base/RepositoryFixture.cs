using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace WebAutopark.Tests.Fixtures.Base
{
    public abstract class RepositoryFixture<TRepository> : IDisposable, IAsyncDisposable
    {
        protected RepositoryFixture(DbConnection dbConnection)
        {
            DbConnection = dbConnection;
            InitDatabase();
        }
        public DbConnection DbConnection { get; }

        public TRepository Repository => CreateRepository();
        
        protected abstract TRepository CreateRepository();
        protected abstract void InitDatabase();

        public void Dispose() => DbConnection.Dispose();
        
        public ValueTask DisposeAsync() => DbConnection.DisposeAsync();
    }
}