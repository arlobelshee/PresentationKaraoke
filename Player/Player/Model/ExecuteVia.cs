// Presentation Karaoke Player
// File: ThreadSpecificExecution.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	public class ExecuteVia
	{
		[NotNull] private readonly TaskFactory _uiThreadTaskFactory;

		private ExecuteVia([NotNull] TaskScheduler scheduler)
		{
			_uiThreadTaskFactory = new TaskFactory(scheduler);
		}

		[NotNull]
		public Task<T> Do<T>([NotNull] Func<Task<T>> createImage)
		{
			return _uiThreadTaskFactory.StartNew(createImage)
				.Unwrap();
		}

		[NotNull]
		public Task<T> Do<T>([NotNull] Func<T> createImage)
		{
			return _uiThreadTaskFactory.StartNew(createImage);
		}

		[NotNull]
		public static ExecuteVia BackgroundWorkers()
		{
			return new ExecuteVia(TaskScheduler.Default);
		}

		[NotNull]
		public static ExecuteVia CurrentThread()
		{
			return new ExecuteVia(TaskScheduler.FromCurrentSynchronizationContext());
		}
	}
}