// Presentation Karaoke Player
// File: _MachineBrains.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _MachineBrains
	{
		[NotNull] private readonly KaraokeMachine _machine;
		[NotNull] private readonly Clock _clock;
		[NotNull] private readonly AsyncLazy<_SlideLibrary> _slideLibrary;
		[CanBeNull] private RecurringEvent _slideAdvancer;

		private _MachineBrains([NotNull] KaraokeMachine machine, [NotNull] Func<Task<_SlideLibrary>> slideLoader,
			[NotNull] Clock clock)
		{
			_machine = machine;
			_clock = clock;
			machine.Brains_TestAccess = this;
			_machine.ShowOptions();
			_machine.SlideAdvanceSpeed = 20;
			_slideLibrary = new AsyncLazy<_SlideLibrary>(slideLoader);
		}

		[NotNull]
		public Task Start()
		{
			return AdvanceSlide();
		}

		[NotNull]
		public Task StartAutoplay()
		{
			_slideAdvancer = _clock.Schedule(TimeSpan.FromSeconds(_machine.SlideAdvanceSpeed), AdvanceSlide);
			return AdvanceSlide();
		}

		[NotNull]
		public async Task AdvanceSlide()
		{
			await _machine.ShowSlide((await _slideLibrary).PickOneRandomSlide());
		}

		public void Pause()
		{
		}

		public void Stop()
		{
			if (_slideAdvancer != null)
			{
				_slideAdvancer.Dispose();
				_slideAdvancer = null;
			}
			_machine.ShowOptions();
		}

		public static void SupplyBrainFor([NotNull] KaraokeMachine machine)
		{
			_ConnectBrainsToMachine(machine, _BuiltInSlides.LoadAllSlides, new _WallClock());
		}

		[NotNull]
		public static _MachineBrains WithTrivialSlidesAndUi(out KaraokeMachine machine, [NotNull] Clock clock)
		{
			machine = new KaraokeMachine(UiControlMaker.Simulated());
			return _ConnectBrainsToMachine(machine, _TrivialTestSlides.LoadAllSlides, clock);
		}

		[NotNull]
		private static _MachineBrains _ConnectBrainsToMachine([NotNull] KaraokeMachine machine,
			[NotNull] Func<Task<_SlideLibrary>> slideLoader, [NotNull] Clock clock)
		{
			var brains = new _MachineBrains(machine, slideLoader, clock);
			machine.Pause.BindTo(brains.Pause);
			machine.AdvanceSlide.BindTo(brains.AdvanceSlide);
			machine.Stop.BindTo(brains.Stop);
			machine.Start.BindTo(brains.Start);
			machine.StartAutoplay.BindTo(brains.StartAutoplay);
			return brains;
		}
	}
}