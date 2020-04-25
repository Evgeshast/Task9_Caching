using System;
using FibonacciNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CachingSolutionsSamples
{
    [TestClass]
    public class FibonacciTests
    {
        [TestMethod]
        public void MemoryCacheTestMethod()
        {
            Calculator calculator = new Calculator(new MemoryCache<int>());
            Console.WriteLine(calculator.CalculateFibonacciNumber(300));
        }

        [TestMethod]
        public void RedisCacheTestMethod()
        {
            Calculator calculator = new Calculator(new RedisCache<int>("localhost"));
            Console.WriteLine(calculator.CalculateFibonacciNumber(300));
        }
    }
}
