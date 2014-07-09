// Presentation Karaoke Player
// File: ExecuteVia.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	public class ExecuteVia
	{
		[NotNull] private static readonly Task<bool> AlreadyCompletedTask = Task.FromResult(true);

		[NotNull]
		public virtual Task<T> Do<T>([NotNull] Func<Task<T>> work)
		{
			return work();
		}

		[NotNull]
		public virtual Task<T> Do<T>([NotNull] Func<T> work)
		{
			return Task.FromResult(work());
		}

		[NotNull]
		public virtual Task Do([NotNull] Action work)
		{
			work();
			return AlreadyCompletedTask;
		}

		[NotNull]
		public static ExecuteVia BackgroundWorkers()
		{
			return new _ExecuteViaAsync(TaskScheduler.Default);
		}

		[NotNull]
		public static ExecuteVia ThisThread()
		{
			return new _ExecuteViaAsync(TaskScheduler.FromCurrentSynchronizationContext());
		}

		[NotNull]
		public static ExecuteVia SynchronousCall()
		{
			return new ExecuteVia();
		}

		private class _ExecuteViaAsync : ExecuteVia
		{
			[NotNull]
			private readonly TaskFactory _uiThreadTaskFactory;

			public _ExecuteViaAsync([NotNull] TaskScheduler scheduler)
			{
				_uiThreadTaskFactory = new TaskFactory(scheduler);
			}

			public override Task<T> Do<T>(Func<Task<T>> work)
			{
				return _uiThreadTaskFactory.StartNew(work)
					.Unwrap();
			}

			public override Task<T> Do<T>(Func<T> work)
			{
				return _uiThreadTaskFactory.StartNew(work);
			}

			public override Task Do(Action work)
			{
				return _uiThreadTaskFactory.StartNew(work);
			}
		}
	}
}