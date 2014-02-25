// Presentation Karaoke Player
// File: HasContent.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using JetBrains.Annotations;

namespace Player.ViewModels
{
	public class HasContent : IValueConverter
	{
		[CanBeNull]
		public object Convert([CanBeNull] object value, [NotNull] Type targetType, [CanBeNull] object parameter, string language)
		{
			if (targetType != typeof (Visibility)) return null;
			return string.IsNullOrWhiteSpace(value as string) ? Visibility.Collapsed : Visibility.Visible;
		}

		[CanBeNull]
		public object ConvertBack([CanBeNull] object value, [NotNull] Type targetType, [CanBeNull] object parameter, string language)
		{
			return null;
		}
	}
}