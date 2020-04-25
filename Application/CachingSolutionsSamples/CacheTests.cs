using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
	[TestClass]
	public class CacheTests
	{
		[TestMethod]
		public void MemoryCache()
		{
			var categoryManager = new EntitiesManager<Category>(new MemoryCache<IEnumerable<Category>>());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCache()
		{
			var categoryManager = new EntitiesManager<Category>(new RedisCache<IEnumerable<Category>>("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

        [TestMethod]
        public void OrdersCache()
        {
            var categoryManager = new EntitiesManager<Order>(new MemoryCache<IEnumerable<Order>>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }


        [TestMethod]
        public void CustomersCache()
        {
            var categoryManager = new EntitiesManager<Customer>(new MemoryCache<IEnumerable<Customer>>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void SqlMonitorCache()
        {
            var categoryManager = new SqlCacheMonitor<Product>(new MemoryCache<IEnumerable<Product>>(), "SELECT TOP (1000) [ProductID],[ProductName],[SupplierID],[CategoryID]," +
                                                                                                       "[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued] " +
                                                                                                       " FROM [Northwind].[dbo].[Products]");

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

	}
}
