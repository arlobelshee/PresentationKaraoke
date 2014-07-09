// Presentation Karaoke Player
// File: PresentationOptionsPage.xaml.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using Windows.UI.Xaml;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Views
{
	/// <summary>
	///    An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class PresentationOptionsPage
	{
		public PresentationOptionsPage()
		{
			InitializeComponent();
		}

		private async void PlayNormal_OnClick([CanBeNull] object sender, [CanBeNull] RoutedEventArgs e)
		{
			await _TheMachine
				.Start.Execute();
		}

		private async void PlayFullAuto_OnClick([CanBeNull] object sender, [CanBeNull] RoutedEventArgs e)
		{
			await _TheMachine
				.StartAutoplay.Execute();
		}

		[NotNull]
		private KaraokeMachine _TheMachine
		{
			get { return (KaraokeMachine) DataContext; }
		}
	}
}