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
        [InlineData(-92, -7540113804746346429)]
        [InlineData(-47, 2971215073)]
        [InlineData(-46, -1836311903)]
        [InlineData(-7, 13)]
        [InlineData(-6, -8)]
        [InlineData(-5, 5)]
        [InlineData(-4, -3)]
        [InlineData(-3, 2)]
        [InlineData(-2, -1)]
        [InlineData(-1, 1)]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        [InlineData(6, 8)]
        [InlineData(7, 13)]
        [InlineData(46, 1836311903)]
        [InlineData(47, 2971215073)]
        [InlineData(92, 7540113804746346429)]
        public async Task GivenGetFibonacci_WhenPassCorrectValuesOfN_ShouldReturnSuccessWithCorrectResult(long n, long expected)
        {
            var client = _server.CreateClient();

            var response = await client.GetAsync($"{EndpointUrl}?n={n}");
            var content = await response.Content.ReadAsStringAsync();
            long.TryParse(content, out var result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-93)]
        [InlineData(93)]
        [InlineData(-9223372036854775808)]
        [InlineData(9223372036854775807)]
        [InlineData(-2147483648)]
        [InlineData(2147483647)]
        public async Task GivenGetFibonacci_WhenPassTooBigValuesOfN_ShouldReturnBadRequest(long n)
        {
            var client = _server.CreateClient();

            var response = await client.GetAsync($"{EndpointUrl}?n={n}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
