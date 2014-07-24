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
	internal class _BuiltInSlides : _SlideLoader
	{
		public const string WhiskeyFileName = "whisky.jpeg";
		private const string WhiskeyName = "whisky";
		private const string CarFileName = "burning_car.jpeg";
		private const string CarName = "car";

		[NotNull] private readonly Lazy<ImageLoader> _imageData;

		public _BuiltInSlides()
		{
			_imageData = new Lazy<ImageLoader>(_InitImageData);
		}

		public Task<_SlideLibrary> LoadAllSlides()
		{
			var allSlides = new[] {_MakeWhiskySlide(), _MakeBurningCarSlide()};
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
		public Slide BurningCar()
		{
			return _MakeBurningCarSlide();
		}

		[NotNull]
		private Slide _MakeBurningCarSlide()
		{
			var result = new Slide(_imageData.Value)
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
		private Slide _MakeWhiskySlide()
		{
			var result = new Slide(_imageData.Value)
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

		public void Dispose()
		{
		}
	}
}