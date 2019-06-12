using System;

namespace Readify.KnockKnock.Api.Services
{
    public class FibonacciService : IFibonacciService
    {
        public long GetSignedNthFibonacciElement(long n)
        {
            if (n == 0) { return 0L; }

            var sign = n < 0 && n % 2 == 0 ? -1 : 1;
            n = Math.Abs(n);

            var f0 = 0L;
            var f1 = 1L;

            for (long i = 2; i <= n; i++)
            {
                var tmp = f0 + f1;
                f0 = f1;
                f1 = tmp;
            }

            return f1 * sign;
        }
    }
}
