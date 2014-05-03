// Presentation Karaoke Player
// File: Slide.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using JetBrains.Annotations;

namespace Player.ViewModels
{
	public class Slide
	{
		[NotNull]
		public BitmapImage Background { get; set; }

		public Stretch BackgroundFill { get; set; }

		public Color BackgroundColor { get; set; }

		[CanBeNull]
		public string MessageTop { get; set; }

		[CanBeNull]
		public string MessageCenter { get; set; }

		[CanBeNull]
		public string MessageBottom { get; set; }

		public Color TextForegroundColor { get; set; }

		public Color TextBackgroundColor { get; set; }

		[NotNull]
		public static async Task<Slide> BurningCar()
		{
			var initialSlide = new Slide
			{
				Background = await _LoadImageFromAsset("burning_car.jpeg"),
				BackgroundFill = Stretch.UniformToFill,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FF000000")
			};
			initialSlide._UseWhiteText();
			return initialSlide;
		}

		[NotNull]
		public static async Task<Slide> Whisky()
		{
			var initialSlide = new Slide
			{
				Background = await _LoadImageFromAsset("whisky.jpeg"),
				BackgroundFill = Stretch.Uniform,
				BackgroundColor = ColorScheme.FromHtmlArgbStringValue("#FFFFFFFF")
			};
			initialSlide._UseBlackText();
			return initialSlide;
		}

		private void _UseWhiteText()
		{
			TextBackgroundColor = ColorScheme.BgBlack;
			TextForegroundColor = ColorScheme.FgWhite;
		}

		private void _UseBlackText()
		{
			TextBackgroundColor = ColorScheme.BgWhite;
			TextForegroundColor = ColorScheme.FgBlack;
		}

		[NotNull]
		private static async Task<BitmapImage> _LoadImageFromAsset([NotNull] string name)
		{
			var uri = new Uri("ms-appx:///Assets/" + name);
			var imageData = await StorageFile.GetFileFromApplicationUriAsync(uri);
			var image = new BitmapImage();
			using (var fileStream = await imageData.OpenAsync(FileAccessMode.Read))
			{
				await image.SetSourceAsync(fileStream);
			}
			return image;
		}
	}
}