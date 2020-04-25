using System;
using CachingSolutionsSamples;

namespace FibonacciNumbers
{
    public class Calculator
    {
        private readonly ICache<int> cache;

        public Calculator(ICache<int> cacheType)
        {
            cache = cacheType;
        }

        public int CalculateFibonacciNumber(int number)
        {
            if (number == 0 || number == 1)
            {
                return number;
            }

            int fromCache = cache.Get(number.ToString());
            if (fromCache != 0)
            {
                Console.WriteLine($"Number in cache {fromCache}");
                return fromCache;
            }

            int result = CalculateFibonacciNumber(number - 1) + CalculateFibonacciNumber(number - 2);
            cache.Set(number.ToString(), result, DateTimeOffset.Now.AddDays(1));
            return result;
        }
    }
}