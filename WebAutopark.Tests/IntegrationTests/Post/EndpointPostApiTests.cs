using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAutopark.Models;
using WebAutopark.Tests.Fixtures.Base;
using Xunit;

namespace WebAutopark.Tests.IntegrationTests.Post
{
    public class EndpointPostApiTests : IntegrationTestFixture
    {
        [Theory]
        [InlineData("/Product/ProductCreate")]
        [InlineData("/Product/ProductDelete")]
        [InlineData("/Product/ProductUpdate")]
        public async Task Post_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var model = new OrderViewModel()
            {

            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}