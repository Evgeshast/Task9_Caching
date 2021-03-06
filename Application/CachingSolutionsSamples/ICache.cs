﻿using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
	public interface ICache<T>
	{
		T Get(string key);
		void Set(string key, T categories, DateTimeOffset expirationTime);
	}
}
