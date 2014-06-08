// Presentation Karaoke Player
// File: _BuiltInSlides.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal static class _BuiltInSlides
	{
		public const string WhiskeyFileName = "whisky.jpeg";
		public const string WhiskeyName = "whisky";
		private const string CarFileName = "burning_car.jpeg";
		private const string CarName = "car";

		[NotNull]
		public static async Task<_SlideLibrary> LoadAllSlides([NotNull] UiControlMaker uiControls)
		{
			var allSlides = await Task.WhenAll(_MakeWhiskySlide(uiControls), BurningCar(uiControls));
			var images = new _ImageLoaderHardCoded();
			images.Add(WhiskeyName, ImageDataFor(WhiskeyFileName));
			images.Add(CarName, ImageDataFor(CarFileName));
			return new _SlideLibrary(allSlides, images, uiControls);
		}

		[NotNull]
		public static async Task<Slide> BurningCar([NotNull] UiControlMaker uiControls)
		{
			var result = new Slide
			{
				Background = await _LoadImage(CarFileName, uiControls),
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
				Background = await _LoadImage(WhiskeyFileName, uiControls),
				BackgroundFill = Stretch.Uniform,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FFFFFFFF"),
				MessageCenter = "Let's play!"
			};
			result.UseBlackText();
			return result;
		}

		[NotNull]
		private static async Task<ImageSource> _LoadImage([NotNull] string file,
			[NotNull] UiControlMaker uiControls)
		{
			using (var fileStream = ImageDataFor(file))
			{
				return await uiControls.CreateImage(fileStream.AsRandomAccessStream());
			}
		}

		[NotNull]
		public static Stream ImageDataFor([NotNull] string name)
		{
			return typeof (_BuiltInSlides).GetTypeInfo()
				.Assembly.GetManifestResourceStream("Player.Assets." + name);
		}

		[NotNull]
		public static Task CopyArbitraryImageToStream([NotNull] Stream destination)
		{
			var fileStream = ImageDataFor(WhiskeyFileName);
			return fileStream.CopyToAsync(destination)
				.ContinueWith(t => fileStream.Dispose());
		}
	}
}