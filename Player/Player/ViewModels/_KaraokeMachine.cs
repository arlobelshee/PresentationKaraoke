// Presentation Karaoke Player
// File: _KaraokeMachine.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using Windows.UI.Popups;
using JetBrains.Annotations;
using Player.Model;
using Player.Views;

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

		[NotNull] public readonly Command Pause;
		[NotNull] public readonly Command AdvanceSlide;
		[NotNull] public readonly Command Start;
		[NotNull] public readonly Command Stop;

		private Slide _currentSlide;
		private Type _currentPageType;
		internal _MachineBrains Brains_TestAccess;

		[NotNull]
		public static KaraokeMachine WithABrain()
		{
			var result = new KaraokeMachine();
			_MachineBrains.SupplyBrainFor(result);
			result.Start.Call();
			return result;
		}

		public KaraokeMachine()
		{
			Pause = new Command(_NoOp);
			AdvanceSlide = new Command(_NoOp);
			Start = new Command(_NoOp);
			Stop = new Command(_NoOp);
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
			AdvanceSlide.BindTo(() => new MessageDialog("This would advance the slide.").ShowAsync());
			Pause.BindTo(() => new MessageDialog("Pausing the presentation.").ShowAsync());
			Stop.BindTo(() => new MessageDialog("This would start a new presentation.").ShowAsync());
			Stop.BindTo(() => new MessageDialog("Stopping the presentation.").ShowAsync());
		}
	}
}