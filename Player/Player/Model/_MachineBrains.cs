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
		private readonly AsyncLazy<_SlideLibrary> _slideLibrary;

		private _MachineBrains([NotNull] KaraokeMachine machine, [NotNull] Func<Task<_SlideLibrary>> slideLoader)
		{
			_machine = machine;
			machine.Brains_TestAccess = this;
			_machine.ShowOptions();
			_machine.SlideAdvanceSpeed = 20;
			_slideLibrary = new AsyncLazy<_SlideLibrary>(slideLoader);
		}

		[NotNull]
		public async Task Start()
		{
			_machine.ShowSlide((await _slideLibrary).PickOneRandomSlide());
		}

		[NotNull]
		public async Task AdvanceSlide()
		{
			_machine.ShowSlide((await _slideLibrary).PickOneRandomSlide());
		}

		public void Pause()
		{
		}

		public void Stop()
		{
			_machine.ShowOptions();
		}

		public static void SupplyBrainFor([NotNull] KaraokeMachine machine)
		{
			_ConnectBrainsToMachine(machine, () => _BuiltInSlides.LoadAllSlides(machine.ControlMaker));
		}

		[NotNull]
		public static _MachineBrains WithTrivialSlidesAndUi(out KaraokeMachine machine)
		{
			machine = new KaraokeMachine(UiControlMaker.Simulated());
			return _ConnectBrainsToMachine(machine, _TrivialTestSlides.LoadAllSlides);
		}

		[NotNull]
		private static _MachineBrains _ConnectBrainsToMachine([NotNull] KaraokeMachine machine,
			[NotNull] Func<Task<_SlideLibrary>> slideLoader)
		{
			var brains = new _MachineBrains(machine, slideLoader);
			machine.Pause.BindTo(brains.Pause);
			machine.AdvanceSlide.BindTo(brains.AdvanceSlide);
			machine.Stop.BindTo(brains.Stop);
			machine.Start.BindTo(brains.Start);
			return brains;
		}
	}
}