// Presentation Karaoke Player
// File: _BuiltInSlides.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
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
			var allSlides = new[] {_MakeWhiskySlide(uiControls), BurningCar()};
			return new _SlideLibrary(allSlides);
		}

		[NotNull]
		public static ImageLoader BuiltInImages()
		{
			var images = new _ImageLoaderHardCoded();
			images.Add(WhiskeyName, ImageDataFor(WhiskeyFileName));
			images.Add(CarName, ImageDataFor(CarFileName));
			return images;
		}

		[NotNull]
		public static Slide BurningCar()
		{
			var result = new Slide
			{
				BackgroundImageName = CarName,
				BackgroundFill = Stretch.UniformToFill,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FF000000"),
				MessageTop = "You are so advanced!"
			};
			result.UseWhiteText();
			return result;
		}

		[NotNull]
		private static Slide _MakeWhiskySlide([NotNull] UiControlMaker uiControls)
		{
			var result = new Slide
			{
				BackgroundImageName = WhiskeyName,
				BackgroundFill = Stretch.Uniform,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FFFFFFFF"),
				MessageCenter = "Let's play!"
			};
			result.UseBlackText();
			return result;
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