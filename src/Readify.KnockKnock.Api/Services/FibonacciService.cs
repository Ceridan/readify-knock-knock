using System;

namespace Readify.KnockKnock.Api.Services
{
    public class FibonacciService : IFibonacciService
    {
        private const int FibonacciLimit = 92;

        public long GetSignedNthFibonacciElement(long n)
        {
            if (n == 0)
            {
                return 0L;
            }

            if (Math.Abs(n) > FibonacciLimit)
            {
                throw new OverflowException($"Value of n should be between {-FibonacciLimit} and {FibonacciLimit}. Current value is: {n}");
            }

            var sign = n < 0 && n % 2 == 0 ? -1 : 1;
            n = Math.Abs(n);

            var f0 = 0L;
            var f1 = 1L;

            for (var i = 2L; i <= n; i++)
            {
                var tmp = f0 + f1;
                f0 = f1;
                f1 = tmp;
            }

            return f1 * sign;
        }
    }
}
