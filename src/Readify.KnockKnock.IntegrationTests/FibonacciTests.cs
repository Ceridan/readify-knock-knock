using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Readify.KnockKnock.IntegrationTests.DSL;
using Xunit;

namespace Readify.KnockKnock.IntegrationTests
{
    public class FibonacciTests
    {
        private const string EndpointUrl = "/api/Fibonacci";
        private readonly TestServer _server;

        public FibonacciTests()
        {
            _server = TestServerFactory.CreateTestServer();
        }

        [Theory]
        [InlineData(-7, 13)]
        public async Task GivenGetFibonacci_WhenPassCorrectValuesOfN_ShouldReturnSuccessWithCorrectResult(long n, long expected)
        {
            var client = _server.CreateClient();

            var response = await client.GetAsync($"{EndpointUrl}?n={n}");
            var content = await response.Content.ReadAsStringAsync();
            long.TryParse(content, out var result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expected, result);
        }
    }
}
