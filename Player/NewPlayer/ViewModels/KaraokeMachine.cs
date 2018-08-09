// Presentation Karaoke Player
// File: KaraokeMachine.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using JetBrains.Annotations;
using Player.Model;
using Player.MvvmHelpers;
using Player.Views;

namespace Player.ViewModels
{
	public class KaraokeMachine : FirePropertyChanged, IDisposable
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
		public AsyncCommand AdvanceSlide { get; private set; }

		[NotNull]
		public AsyncCommand Start { get; private set; }

		[NotNull]
		public AsyncCommand StartAutoplay { get; private set; }

		[NotNull]
		public AsyncCommand TurnOn { get; private set; }

		[NotNull]
		public Command Stop { get; private set; }

		[NotNull]
		internal Action _CleanUp { get; set; }

		[NotNull]
		public UiControlMaker ControlMaker { get; private set; }

		private Slide _currentSlide;
		private Type _currentPageType;

		private int _slideAdvanceSpeed;

		[NotNull]
		public static KaraokeMachine WithABrain()
		{
			var result = new KaraokeMachine();
			_MachineBrains.SupplyBrainFor(result);
			return result;
		}

		[NotNull]
		public static KaraokeMachine Brainless()
		{
			return new KaraokeMachine(ExecuteVia.SynchronousCall());
		}

		protected KaraokeMachine()
			: this(ExecuteVia.ThisThread())
		{
		}

		public KaraokeMachine([NotNull] ExecuteVia uiThread) : base(uiThread)
		{
			ControlMaker = new UiControlMaker(uiThread);
			Pause = new Command(_NoOp);
			AdvanceSlide = AsyncCommand.Wrapping(_NoOp);
			Start = AsyncCommand.Wrapping(_NoOp);
			StartAutoplay = AsyncCommand.Wrapping(_NoOp);
			TurnOn = AsyncCommand.Wrapping(_NoOp);
			Stop = new Command(_NoOp);
			_CleanUp = _NoOp;
		}

		private static void _NoOp()
		{
		}

		public void ShowSlide([NotNull] Slide nextSlide)
		{
			CurrentSlide = nextSlide;
			CurrentPageType = typeof (PresentationPlayingPage);
		}

		public void ShowOptions()
		{
			CurrentPageType = typeof (PresentationOptionsPage);
		}

		public void Dispose()
		{
			_CleanUp();
		}
	}

	public class KaraokeMachine_PlayPresentation_DesignData : KaraokeMachine
	{
		public KaraokeMachine_PlayPresentation_DesignData()
		{
			var initialSlide = _BuiltInSlides.BurningCar();
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