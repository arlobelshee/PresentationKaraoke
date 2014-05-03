// Presentation Karaoke Player
// File: NavigationTarget.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using Windows.UI.Xaml.Input;
using JetBrains.Annotations;

namespace Player.Views
{
	public interface NavigationTarget
	{
		void OnKey([NotNull] object sender, [NotNull] KeyRoutedEventArgs e);
	}
}