// Presentation Karaoke Player
// File: UiControlMaker.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
		public Task<ImageSource> CreateImage([NotNull] IRandomAccessStream imageData)
		{
			return _uiThreadTaskFactory.StartNew(async () => await _MakeImage(imageData))
				.Unwrap();
		}

		[NotNull]
		protected virtual async Task<ImageSource> _MakeImage([NotNull] IRandomAccessStream imageData)
		{
			var image = new BitmapImage();
			await image.SetSourceAsync(imageData);
			return image;
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

			protected override Task<ImageSource> _MakeImage(IRandomAccessStream imageData)
			{
				return Task.FromResult((ImageSource) new _FakeImage(imageData));
			}
		}

		internal class _FakeImage : ImageSource
		{
			[NotNull]
			public IRandomAccessStream ImageData { get; private set; }

			public _FakeImage([NotNull] IRandomAccessStream imageData)
			{
				ImageData = imageData;
			}
		}
	}
}