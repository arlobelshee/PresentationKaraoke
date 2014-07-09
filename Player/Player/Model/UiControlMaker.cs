// Presentation Karaoke Player
// File: UiControlMaker.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using JetBrains.Annotations;
using Player.ViewModels;

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
		public Task<BitmapImage> CreateImage([NotNull] IRandomAccessStream imageData)
		{
			return _uiThreadTaskFactory.StartNew(async () =>
			{
				var image = new BitmapImage();
				await image.SetSourceAsync(imageData);
				return image;
			})
				.Unwrap();
		}

		[NotNull]
		public static UiControlMaker Simulated()
		{
			return new UiControlMaker(TaskScheduler.Default);
		}

		[NotNull]
		public async Task<Slide> Inflate([NotNull] Slide slide)
		{
			if (string.IsNullOrEmpty(slide.BackgroundImageName)) return slide;
			var slideImageData = slide.ImageData.Load(slide.BackgroundImageName);
			slide.Background = await CreateImage(slideImageData.AsRandomAccessStream());
			return slide;
		}

		public void Deflate([CanBeNull] Slide slide)
		{
			if (slide == null) return;
			slide.Background = null;
		}
	}
}