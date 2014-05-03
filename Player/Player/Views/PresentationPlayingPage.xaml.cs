// Presentation Karaoke Player
// File: PresentationPlayingPage.xaml.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Views
{
	/// <summary>
	///    An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class PresentationPlayingPage : NavigationTarget
	{
		private static readonly VirtualKey[] AdvanceSlideKeys =
		{
			VirtualKey.Enter,
			VirtualKey.Space,
			VirtualKey.Left,
			VirtualKey.Right,
			VirtualKey.Down,
			VirtualKey.Up
		};

		public PresentationPlayingPage()
		{
			InitializeComponent();
		}

		public void OnKey(object sender, KeyRoutedEventArgs e)
		{
			if (AdvanceSlideKeys.Contains(e.Key))
				_TheMachine.AdvanceSlide.Call();
		}

		[NotNull]
		private KaraokeMachine _TheMachine
		{
			get { return (KaraokeMachine) DataContext; }
		}
	}
}