using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Readify.KnockKnock.IntegrationTests.DSL;
using Xunit;

namespace Readify.KnockKnock.IntegrationTests
{
    public class TriangleTypeTests
    {
        private const string EndpointUrl = "/api/TriangleType";
        private readonly TestServer _server;

        public TriangleTypeTests()
        {
            _server = TestServerFactory.CreateTestServer();
        }

        [Theory]
        [InlineData(-2147483648, -2147483648, -2147483648, "Error")]
        [InlineData(-1, -1, -1, "Error")]
        [InlineData(-1, 1, 1, "Error")]
        [InlineData(1, -1, 1, "Error")]
        [InlineData(1, 1, -1, "Error")]
        [InlineData(0, 0, 0, "Error")]
        [InlineData(0, 1, 1, "Error")]
        [InlineData(1, 0, 1, "Error")]
        [InlineData(1, 1, 0, "Error")]
        [InlineData(1, 1, 2, "Error")]
        [InlineData(1, 2, 1, "Error")]
        [InlineData(2, 1, 1, "Error")]
        [InlineData(1, 2, 3, "Error")]
        [InlineData(1, 1, 2147483647, "Error")]
        [InlineData(1, 1, 1, "Equilateral")]
        [InlineData(2, 2, 2, "Equilateral")]
        [InlineData(2147483647, 2147483647, 2147483647, "Equilateral")]
        [InlineData(2, 2, 3, "Isosceles")]
        [InlineData(2, 3, 2, "Isosceles")]
        [InlineData(3, 2, 2, "Isosceles")]
        [InlineData(2, 3, 4, "Scalene")]
        [InlineData(3, 4, 2, "Scalene")]
        [InlineData(4, 2, 3, "Scalene")]
        [InlineData(4, 3, 2, "Scalene")]
        public async Task GivenGetTriangleType_WhenPassSidesOfAnyLength_ShouldReturnSuccessWithCorrectResult(
            int a,
            int b,
            int c,
            string expected)
        {
            var client = _server.CreateClient();

            var response = await client.GetAsync($"{EndpointUrl}?a={a}&b={b}&c={c}");
            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expected, result);
        }
    }
}
