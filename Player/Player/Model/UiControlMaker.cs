// Presentation Karaoke Player
// File: UiControlMaker.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	public class UiControlMaker
	{
		private readonly TaskFactory _uiThreadTaskFactory;

		public UiControlMaker() : this(TaskScheduler.FromCurrentSynchronizationContext())
		{
		}

		private UiControlMaker([NotNull] TaskScheduler scheduler)
		{
			_uiThreadTaskFactory = new TaskFactory(scheduler);
		}

		[NotNull]
		public virtual InflatableImageData CreateImage([NotNull] string name, [NotNull] ImageLoader imageData)
		{
			return new InflatableImageDataUiThreaded(name, imageData, _uiThreadTaskFactory);
		}

		[NotNull]
		public static UiControlMaker Simulated()
		{
			return new _OnWorkerThreadPool();
		}

		private class _OnWorkerThreadPool : UiControlMaker
		{
			public _OnWorkerThreadPool()
				: base(TaskScheduler.Default)
			{
			}

			[NotNull]
			public override InflatableImageData CreateImage(string name, ImageLoader imageData)
			{
				return new _InflatableImageDataLocalThreaded(name, imageData);
			}
		}
	}
}