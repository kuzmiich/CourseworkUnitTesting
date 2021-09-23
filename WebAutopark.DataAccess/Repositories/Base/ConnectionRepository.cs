using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace WebAutopark.DataAccess.Repositories.Base
{
    public abstract class ConnectionRepository : IDisposable, IAsyncDisposable
    {
        protected readonly DbConnection DbConnection;
        
        protected ConnectionRepository(DbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public void Dispose() => DbConnection.Dispose();

        public ValueTask DisposeAsync() => DbConnection.DisposeAsync();
    }
}
