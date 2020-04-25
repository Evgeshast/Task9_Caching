using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
	public class EntitiesManager<T> where T :class
	{
		private ICache<IEnumerable<T>> cache;

		public EntitiesManager(ICache<IEnumerable<T>> cache)
		{
			this.cache = cache;
		}

		public IEnumerable<T> GetEntities()
		{
			Console.WriteLine("Get Categories");

			var user = Thread.CurrentPrincipal.Identity.Name;
			var entities = cache.Get(user);

			if (entities == null)
			{
				Console.WriteLine("From DB");

				using (var dbContext = new Northwind())
				{
					dbContext.Configuration.LazyLoadingEnabled = false;
					dbContext.Configuration.ProxyCreationEnabled = false;
					entities = dbContext.Set<T>().ToList();
                    cache.Set(user, entities, DateTime.Now.AddMilliseconds(3000));
				}
            }

			return entities;
		}
	}
}
