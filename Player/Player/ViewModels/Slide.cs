// Presentation Karaoke Player
// File: Slide.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using JetBrains.Annotations;
using Player.Model;

namespace Player.ViewModels
{
	public class Slide
	{
		[CanBeNull]
		public string BackgroundImageName { get; set; }

		[CanBeNull]
		public ImageSource Background { get; set; }

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

		public void UseWhiteText()
		{
			TextBackgroundColor = ColorScheme.BgBlack;
			TextForegroundColor = ColorScheme.FgWhite;
		}

		public void UseBlackText()
		{
			TextBackgroundColor = ColorScheme.BgWhite;
			TextForegroundColor = ColorScheme.FgBlack;
		}
	}
}