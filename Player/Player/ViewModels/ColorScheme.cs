// Presentation Karaoke Player
// File: TextColorScheme.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using JetBrains.Annotations;

namespace Player.ViewModels
{
	public class ColorScheme : IValueConverter
	{
		public static Color BgBlack = Color.FromArgb(153, 0, 0, 0);
		public static Color FgBlack = Color.FromArgb(255, 0, 0, 0);
		public static Color BgWhite = Color.FromArgb(153, 255, 255, 255);
		public static Color FgWhite = Color.FromArgb(255, 255, 255, 255);

		[NotNull]
		public object Convert([CanBeNull] object value, [NotNull] Type targetType, [CanBeNull] object parameter,
			string language)
		{
			if (!(value is Color))
			{
				value = Colors.Violet;
			}
			var color = (Color) value;
			return new SolidColorBrush(color);
		}

		[NotNull]
		public object ConvertBack([CanBeNull] object value, [NotNull] Type targetType, [CanBeNull] object parameter,
			string language)
		{
			return null;
		}

		public static Color FromHtmlArgbStringValue([NotNull] string argb)
		{
			argb = argb.TrimStart('#');
			byte alpha = Byte.Parse(argb.Substring(0, 2), NumberStyles.HexNumber);
			byte red = Byte.Parse(argb.Substring(2, 2), NumberStyles.HexNumber);
			byte green = Byte.Parse(argb.Substring(4, 2), NumberStyles.HexNumber);
			byte blue = Byte.Parse(argb.Substring(6, 2), NumberStyles.HexNumber);
			return Color.FromArgb(alpha, red, green, blue);
		}
	}
}