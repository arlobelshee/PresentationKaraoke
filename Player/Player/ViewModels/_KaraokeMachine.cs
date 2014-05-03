// Presentation Karaoke Player
// File: _KaraokeMachine.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
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

		public int SlideAdvanceSpeed
		{
			get { return _slideAdvanceSpeed; }
			set
			{
				if (value == _slideAdvanceSpeed) return;
				_slideAdvanceSpeed = value;
				NotifyChangeWatchers();
			}
		}

		[NotNull]
		public Command Pause { get; private set; }

		[NotNull]
		public Command AdvanceSlide { get; private set; }

		[NotNull]
		public Command Start { get; private set; }

		[NotNull]
		public Command Stop { get; private set; }

		private Slide _currentSlide;
		private Type _currentPageType;
		private int _slideAdvanceSpeed;
		internal _MachineBrains Brains_TestAccess;

		[NotNull]
		public static KaraokeMachine WithABrain()
		{
			var result = new KaraokeMachine();
			_MachineBrains.SupplyBrainFor(result);
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

		public void ShowOptions()
		{
			CurrentPageType = typeof (PresentationOptionsPage);
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
		}
	}

	public class KaraokeMachine_ShowOptions_DesignData : KaraokeMachine
	{
		public KaraokeMachine_ShowOptions_DesignData()
		{
			ShowOptions();
		}
	}
}