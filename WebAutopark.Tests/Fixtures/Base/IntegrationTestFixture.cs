using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebAutopark.Tests.Fixtures.Base
{
    public class IntegrationTestFixture : IClassFixture<WebApplicationFactory<Startup>>, IDisposable, IAsyncDisposable
    {
        protected readonly WebApplicationFactory<Startup> Factory;

        protected IntegrationTestFixture()
        {
            Factory = new WebApplicationFactory<Startup>();
        }
        
        public void Dispose() => Factory.Dispose();

        public async ValueTask DisposeAsync()
        {
            await Task.Run(() =>
            {
                Factory.Dispose();
                return Task.FromResult(true);
            });
        }
    }
}