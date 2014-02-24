// Presentation Karaoke Player
// File: _KaraokeMachine.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using JetBrains.Annotations;

namespace Player.Model
{
	public class KaraokeMachine : FirePropertyChanged
	{
		[NotNull]
		public Type CurrentPageType
		{
			get { return _currentPageType; }
			set
			{
				if (value == _currentPageType) return;
				_currentPageType = value;
				NotifyChangeWatchers();
			}
		}

		[NotNull]
		public Presentation NowPlaying
		{
			get { return _nowPlaying; }
			set
			{
				if (value == _nowPlaying) return;
				_nowPlaying = value;
				NotifyChangeWatchers();
			}
		}

		[NotNull] public Action Pause;

		private Presentation _nowPlaying;
		private Type _currentPageType;

		[NotNull]
		public static KaraokeMachine BoundToModel()
		{
			return new KaraokeMachine {_currentPageType = typeof (PresentationPlayingPage), _nowPlaying = new Presentation(new Slide())};
		}

		public KaraokeMachine()
		{
			Pause = _NoOp;
		}

		private static void _NoOp()
		{
		}
	}

	public class DesignDataMachine_PlayPresentation : KaraokeMachine
	{
		public DesignDataMachine_PlayPresentation()
		{
			var initialSlide = new Slide {Background = "Assets/burning_car.jpeg"};
			NowPlaying = new Presentation(initialSlide);
			CurrentPageType = typeof (PresentationPlayingPage);
		}
	}
}