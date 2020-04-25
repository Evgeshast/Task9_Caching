using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindLibrary;
using StackExchange.Redis;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace CachingSolutionsSamples
{
	class RedisCache<T> : ICache<T>
	{
		private ConnectionMultiplexer redisConnection;
		string prefix = "Cache";
		DataContractSerializer serializer = new DataContractSerializer(
			typeof(T));

		public RedisCache(string hostName)
		{
			ConfigurationOptions option = new ConfigurationOptions
			{
				AbortOnConnectFail = false,
                DefaultDatabase = 0,
				EndPoints = { hostName, "0" }
			};
			redisConnection = ConnectionMultiplexer.Connect(option);
		}

		public T Get(string key)
		{
			var db = redisConnection.GetDatabase();
			byte[] s = db.StringGet(key);
			if (s == null)
				return default(T);

			return (T) serializer.ReadObject(new MemoryStream(s));

		}

		public void Set(string key, T categories, DateTimeOffset expirationTime)
		{
			var db = redisConnection.GetDatabase();

            if (categories == null)
			{
				db.StringSet(key, RedisValue.Null, expirationTime - DateTimeOffset.Now);
			}
			else
			{
				var stream = new MemoryStream();
				serializer.WriteObject(stream, categories);
				db.StringSet(key, stream.ToArray());
			}
		}
	}
}
