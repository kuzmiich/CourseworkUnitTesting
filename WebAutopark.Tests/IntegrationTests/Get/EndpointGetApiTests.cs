using System.Net;
using System.Threading.Tasks;
using WebAutopark.Tests.Fixtures.Base;
using Xunit;

namespace WebAutopark.Tests.IntegrationTests.Get
{
    public class EndpointGetApiTests : IntegrationTestFixture
    {
        [Theory]
        [InlineData("/")]
        [InlineData("/Privacy")]
        [InlineData("/Error")]
        [InlineData("/products/index")]
        [InlineData("/products/create")]
        [InlineData("/products/delete")]
        [InlineData("/products/update")]
        [InlineData("/products/info")]
        [InlineData("/orders/index")]
        [InlineData("/orders/delete")]
        [InlineData("/orders/update")]
        [InlineData("/orders/create")]
        [InlineData("/carts/index")]
        [InlineData("/carts/add")]
        [InlineData("/carts/remove")]
        [InlineData("/carts/clear")]
        [InlineData("/users/index")]
        [InlineData("/users/delete")]
        [InlineData("/Account/Login")]
        [InlineData("/Account/Register")]
        [InlineData("/Account/Logout")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
        
        [Theory]
        [InlineData("/Query")]
        public async Task Get_EndpointsReturnErrorAndNotFoundContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(response.Content.Headers.ContentType);
        }
    }
}