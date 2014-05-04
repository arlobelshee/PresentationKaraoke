// Presentation Karaoke Player
// File: _BuiltInSlides.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal static class _BuiltInSlides
	{
		[NotNull]
		public static async Task<Slide> BurningCar([NotNull] UiControlMaker uiControls)
		{
			var result = new Slide
			{
				Background = await _LoadImage("burning_car.jpeg", uiControls),
				BackgroundFill = Stretch.UniformToFill,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FF000000"),
				MessageTop = "You are so advanced!"
			};
			result.UseWhiteText();
			return result;
		}

		[NotNull]
		public static async Task<_SlideLibrary> LoadAllSlides([NotNull] UiControlMaker uiControls)
		{
			var allSlides = await Task.WhenAll(_MakeWhiskySlide(uiControls), BurningCar(uiControls));
			return new _SlideLibrary(allSlides);
		}

		[NotNull]
		private static async Task<Slide> _MakeWhiskySlide([NotNull] UiControlMaker uiControls)
		{
			var result = new Slide
			{
				Background = await _LoadImage("whisky.jpeg", uiControls),
				BackgroundFill = Stretch.Uniform,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FFFFFFFF"),
				MessageCenter = "Let's play!"
			};
			result.UseBlackText();
			return result;
		}

		[NotNull]
		private static async Task<BitmapImage> _LoadImage([NotNull] string name, [NotNull] UiControlMaker uiControls)
		{
			var uri = new Uri("ms-appx:///Assets/" + name);
			var imageData = await StorageFile.GetFileFromApplicationUriAsync(uri);
			using (var fileStream = await imageData.OpenAsync(FileAccessMode.Read))
			{
				return await uiControls.CreateImage(fileStream);
			}
		}
	}
}