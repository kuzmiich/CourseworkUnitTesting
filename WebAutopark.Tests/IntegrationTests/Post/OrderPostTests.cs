using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAutopark.Models;
using WebAutopark.Tests.Fixtures.Base;
using Xunit;

namespace WebAutopark.Tests.IntegrationTests.Post
{
    public class OrderPostTests : IntegrationTestFixture
    {
        [Fact]
        public async Task Post_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            const string url = "/Order/OrderCreate";

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