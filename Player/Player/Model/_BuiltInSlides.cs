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
		public const string WhiskeyName = "whisky.jpeg";
		private const string CarName = "burning_car.jpeg";

		[NotNull]
		public static async Task<_SlideLibrary> LoadAllSlides([NotNull] UiControlMaker uiControls)
		{
			var allSlides = await Task.WhenAll(_MakeWhiskySlide(uiControls), BurningCar(uiControls));
			return new _SlideLibrary(allSlides, null);
		}

		[NotNull]
		public static async Task<Slide> BurningCar([NotNull] UiControlMaker uiControls)
		{
			var result = new Slide
			{
				Background = await _LoadImage(CarName, uiControls),
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
				Background = await _LoadImage(WhiskeyName, uiControls),
				BackgroundFill = Stretch.Uniform,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FFFFFFFF"),
				MessageCenter = "Let's play!"
			};
			result.UseBlackText();
			return result;
		}

		[NotNull]
		private static async Task<BitmapImage> _LoadImage([NotNull] string file,
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
			var fileStream = ImageDataFor(WhiskeyName);
			return fileStream.CopyToAsync(destination)
				.ContinueWith(t => fileStream.Dispose());
		}
	}
}