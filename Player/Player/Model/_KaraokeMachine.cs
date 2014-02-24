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
		public Slide CurrentSlide
		{
			get { return _currentSlide; }
			set
			{
				if (value == _currentSlide) return;
				_currentSlide = value;
				NotifyChangeWatchers();
			}
		}

		[NotNull] public Action Pause;

		private Slide _currentSlide;
		private Type _currentPageType;

		[NotNull]
		public static KaraokeMachine BoundToModel()
		{
			var result = new KaraokeMachine();
			var brains = new _MachineBrains(result);
			brains.BeginPresentation();
			return result;
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
			CurrentSlide = initialSlide;
			CurrentPageType = typeof (PresentationPlayingPage);
		}
	}
}