// Presentation Karaoke Player
// File: _UiControlCreation.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using JetBrains.Annotations;

namespace Player.Model
{
	public class UiControlMaker
	{
		private readonly TaskFactory _uiThreadTaskFactory;

		public UiControlMaker(): this(TaskScheduler.FromCurrentSynchronizationContext())
		{
		}

		private UiControlMaker([NotNull] TaskScheduler scheduler)
		{
			_uiThreadTaskFactory = new TaskFactory(scheduler);
		}

		[NotNull]
		public Task<BitmapImage> CreateImage([NotNull] IRandomAccessStream imageData)
		{
			return _uiThreadTaskFactory.StartNew(async () =>
			{
				var image = new BitmapImage();
				await image.SetSourceAsync(imageData);
				return image;
			}).Unwrap();
		}

		[NotNull]
		public static UiControlMaker Simulated()
		{
			return new UiControlMaker(TaskScheduler.Default);
		}
	}
}