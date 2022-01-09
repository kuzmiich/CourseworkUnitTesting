using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAutopark.DataAccess;

namespace WebAutopark.Tests.Fixtures.Base
{
    public abstract class RepositoryFixture<TRepository> : IDisposable, IAsyncDisposable
    {
        internal RepositoryFixture()
        {
            Connection = ContextCreator.CreateContext();
            Repository = CreateRepository();
            InitDatabase().GetAwaiter();
        }

        public int Id { get; set; }
        public WebAutoparkContext Connection { get; }
        public TRepository Repository { get; }
        
        protected abstract TRepository CreateRepository();
        protected abstract Task InitDatabase();

        public void Dispose() => Connection.Dispose();
        public ValueTask DisposeAsync() => Connection.DisposeAsync();
    }
}