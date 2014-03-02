// Presentation Karaoke Player
// File: _KaraokeMachine.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using Windows.UI.Popups;
using JetBrains.Annotations;
using Player.Model;

namespace Player.ViewModels
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
		[NotNull] public Action AdvanceSlide;

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
			AdvanceSlide = _NoOp;
		}

		private static void _NoOp()
		{
		}

		public void ShowSlide([NotNull] Slide initialSlide)
		{
			CurrentSlide = initialSlide;
			CurrentPageType = typeof (PresentationPlayingPage);
		}
	}

	public class KaraokeMachine_PlayPresentation_DesignData : KaraokeMachine
	{
		public KaraokeMachine_PlayPresentation_DesignData()
		{
			var initialSlide = Slide.BurningCar();
			initialSlide.MessageBottom = "Witty down low";
			initialSlide.MessageCenter = null;
			initialSlide.MessageTop = "Smart and funny at the top";
			ShowSlide(initialSlide);
			AdvanceSlide = () => new MessageDialog("This would advance the slide.").ShowAsync();
			Pause = () => new MessageDialog("Pausing the presentation.").ShowAsync();
		}
	}
}