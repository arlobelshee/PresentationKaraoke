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
		[NotNull] private readonly ExecuteVia _uiThread;

		public UiControlMaker([NotNull] ExecuteVia uiThread)
		{
			_uiThread = uiThread;
		}

		[NotNull]
		private static async Task<BitmapImage> _CreateImage([NotNull] IRandomAccessStream imageData)
		{
			var image = new BitmapImage();
			await image.SetSourceAsync(imageData);
			return image;
		}

		[NotNull]
		public async Task<Slide> Inflate([NotNull] Slide slide)
		{
			if (string.IsNullOrEmpty(slide.BackgroundImageName)) return slide;
			var slideImageData = (await slide.ImageData.Load(slide.BackgroundImageName)).AsRandomAccessStream();
			slide.Background = await _uiThread.Do(() => _CreateImage(slideImageData));
			return slide;
		}

		public void Deflate([CanBeNull] Slide slide)
		{
			if (slide == null) return;
			slide.Background = null;
		}
	}
}