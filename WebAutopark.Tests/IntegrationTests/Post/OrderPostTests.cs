using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAutopark.Models;
using WebAutopark.Tests.Fixtures.Base;
using Xunit;
using Xunit.Abstractions;

namespace WebAutopark.Tests.IntegrationTests.Post
{
    public class OrderPostTests : IntegrationTestFixture
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public OrderPostTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Post_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange
            const string url = "/orders/delete";

            var model = new OrderViewModel()
            {
            };

            var client = Factory.CreateClient();

            // Act
            var stringContent =
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent);
            _testOutputHelper.WriteLine(response.Content.ToString());
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType!.ToString());
        }

        [Theory]
        [InlineData("/Product/E")]
        public async Task Post_EndpointsReturnErrorAndNotFoundContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var model = new OrderViewModel()
            {
            };
            var stringContent =
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(response.Content.Headers.ContentType);
        }
    }
}