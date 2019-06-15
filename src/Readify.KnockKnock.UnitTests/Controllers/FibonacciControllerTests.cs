using System;
using Microsoft.AspNetCore.Mvc;
using Readify.KnockKnock.UnitTests.DSL;
using Xunit;

namespace Readify.KnockKnock.UnitTests.Controllers
{
    public class FibonacciControllerTests
    {
        [Fact]
        public async void GivenQueryParamIsSmallInteger_ThenReturnsNthFibonacciNumber()
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
    }
}
