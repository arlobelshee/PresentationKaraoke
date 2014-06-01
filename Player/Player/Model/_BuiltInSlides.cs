// Presentation Karaoke Player
// File: _BuiltInSlides.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal static class _BuiltInSlides
	{
		[NotNull] private static readonly AsyncLazy<StorageFile> Whisky =
			new AsyncLazy<StorageFile>(() => _LoadImageData("whisky.jpeg"));

		[NotNull] private static readonly AsyncLazy<StorageFile> Car =
			new AsyncLazy<StorageFile>(() => _LoadImageData("burning_car.jpeg"));

		[NotNull]
		public static async Task<_SlideLibrary> LoadAllSlides([NotNull] UiControlMaker uiControls)
		{
			var allSlides = await Task.WhenAll(_MakeWhiskySlide(uiControls), BurningCar(uiControls));
			return new _SlideLibrary(allSlides);
		}

		[NotNull]
		public static async Task<Slide> BurningCar([NotNull] UiControlMaker uiControls)
		{
			var result = new Slide
			{
				Background = await _LoadImageFrom(Car, uiControls),
				BackgroundFill = Stretch.UniformToFill,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FF000000"),
				MessageTop = "You are so advanced!"
			};
			result.UseWhiteText();
			return result;
		}

		[NotNull]
		private static async Task<Slide> _MakeWhiskySlide([NotNull] UiControlMaker uiControls)
		{
			var result = new Slide
			{
				Background = await _LoadImageFrom(Whisky, uiControls),
				BackgroundFill = Stretch.Uniform,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FFFFFFFF"),
				MessageCenter = "Let's play!"
			};
			result.UseBlackText();
			return result;
		}

		[NotNull]
		public static async Task CopyImageToStream([NotNull] Stream destination)
		{
			using (var fileStream = await (await Whisky).OpenAsync(FileAccessMode.Read))
			{
				await fileStream.AsStreamForRead()
					.CopyToAsync(destination);
			}
		}

		[NotNull]
		private static async Task<BitmapImage> _LoadImageFrom([NotNull] AsyncLazy<StorageFile> file,
			[NotNull] UiControlMaker uiControls)
		{
			using (var fileStream = await (await file).OpenAsync(FileAccessMode.Read))
			{
				return await uiControls.CreateImage(fileStream);
			}
		}

		[NotNull]
		private static async Task<StorageFile> _LoadImageData([NotNull] string name)
		{
			var uri = new Uri("ms-appx:///Assets/" + name);
			return await StorageFile.GetFileFromApplicationUriAsync(uri);
		}
	}
}