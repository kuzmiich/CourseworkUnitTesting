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
        [InlineData("/Product")]
        [InlineData("/Product/ProductCreate")]
        [InlineData("/Product/ProductDelete")]
        [InlineData("/Product/ProductUpdate")]
        [InlineData("/Product/ProductInfo/")]
        [InlineData("/Order")]
        [InlineData("/Order/OrderInfo/")]
        [InlineData("/Order/OrderDelete")]
        [InlineData("/Order/OrderUpdate")]
        [InlineData("/Order/OrderCreate")]
        [InlineData("/ShoppingCart")]
        [InlineData("/User")]
        [InlineData("/User/UserDelete/")]
        [InlineData("/Account/Login")]
        [InlineData("/Account/Register")]
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
        [InlineData("/Account/Regidas")]
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