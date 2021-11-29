using System;
using Microsoft.EntityFrameworkCore;
using WebAutopark.DataAccess;

namespace WebAutopark.Tests.Fixtures.Base
{
    internal static class ContextCreator
    {
        private static readonly DbContextOptions<WebAutoparkContext> Options =
            new DbContextOptionsBuilder<WebAutoparkContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        public static WebAutoparkContext CreateContext() => new (Options);
    }
}