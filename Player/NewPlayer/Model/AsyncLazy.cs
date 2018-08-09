// Presentation Karaoke Player
// File: AsyncLazy.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Player.Model
{
	public class AsyncLazy<T> : Lazy<Task<T>>
	{
		public AsyncLazy(Func<T> valueFactory) :
			base(() => Task.Factory.StartNew(valueFactory))
		{
			_ForceInitToStart();
		}

		public AsyncLazy(Func<Task<T>> taskFactory) :
			base(() => Task.Factory.StartNew(taskFactory)
				.Unwrap())
		{
			_ForceInitToStart();
		}

		public TaskAwaiter<T> GetAwaiter()
		{
			return Value.GetAwaiter();
		}

		private void _ForceInitToStart()
		{
			var foo = Value;
		}
	}
}