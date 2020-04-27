using Readify.KnockKnock.Controllers;
using Readify.KnockKnock.Services;

namespace Readify.KnockKnock.UnitTests.DSL
{
    public class FibonacciControllerBuilder
    {
        private IFibonacciService _fibonacciService;

        public FibonacciControllerBuilder WithRealFibonacciService()
        {
            _fibonacciService = new FibonacciService();
            return this;
        }

        public FibonacciController Please()
        {
            return new FibonacciController(_fibonacciService);
        }
    }
}
