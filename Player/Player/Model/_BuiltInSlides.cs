// Presentation Karaoke Player
// File: _BuiltInSlides.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
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
		private const string WhiskeyName = "whisky";
		private const string CarFileName = "burning_car.jpeg";
		private const string CarName = "car";

		private static readonly Lazy<ImageLoader> ImageData = new Lazy<ImageLoader>(_InitImageData);

		[NotNull]
		public static Task<_SlideLibrary> LoadAllSlides()
		{
			var allSlides = new[] {_MakeWhiskySlide(ImageData.Value), _MakeBurningCarSlide(ImageData.Value)};
			return Task.FromResult(new _SlideLibrary(allSlides));
		}

		[NotNull]
		private static _ImageLoaderHardCoded _InitImageData()
		{
			var imageData = new _ImageLoaderHardCoded();
			imageData.Add(WhiskeyName, ImageDataFor(WhiskeyFileName));
			imageData.Add(CarName, ImageDataFor(CarFileName));
			return imageData;
		}

		[NotNull]
		public static Slide BurningCar()
		{
			return _MakeBurningCarSlide(ImageData.Value);
		}

		[NotNull]
		private static Slide _MakeBurningCarSlide([NotNull] ImageLoader imageData)
		{
			var result = new Slide(imageData)
			{
				BackgroundImageName = CarFileName,
				BackgroundFill = Stretch.UniformToFill,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FF000000"),
				MessageTop = "You are so advanced!"
			};
			result.UseWhiteText();
			return result;
		}

		[NotNull]
		private static Slide _MakeWhiskySlide([NotNull] ImageLoader imageData)
		{
			var result = new Slide(imageData)
			{
				BackgroundImageName = WhiskeyFileName,
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