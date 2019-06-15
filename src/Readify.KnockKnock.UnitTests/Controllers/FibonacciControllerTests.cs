using Microsoft.AspNetCore.Mvc;
using Readify.KnockKnock.UnitTests.DSL;
using Xunit;

namespace Readify.KnockKnock.UnitTests.Controllers
{
    public class FibonacciControllerTests
    {
        [Fact]
        public async void WhenCallGetFibonacciMethod_WithSmallIntegerAsAQueryParam_ThenReturnsNthFibonacciNumber()
        {
            var controller = Create
                .FibonacciController
                .WithRealFibonacciService()
                .Please();

            var response = await controller.GetFibonacci(n: 6);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(8L, okResult.Value);
        }

        [Fact]
        public async void WhenCallGetFibonacciMethod_WithVeryBigLongValue_ThenReturnsBadRequest()
        {
            var controller = Create
                .FibonacciController
                .WithRealFibonacciService()
                .Please();

            var response = await controller.GetFibonacci(n: -9223372036854775808);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("no content", badRequestResult.Value);
        }
    }
}
