// Presentation Karaoke Player
// File: _MachineBrains.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.Threading.Tasks;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _MachineBrains
	{
		[NotNull] private readonly KaraokeMachine _machine;

		private _MachineBrains([NotNull] KaraokeMachine machine)
		{
			_machine = machine;
			machine.Brains_TestAccess = this;
			_machine.ShowOptions();
			_machine.SlideAdvanceSpeed = 20;
		}

		[NotNull]
		public async Task Start()
		{
			var initialSlide = await Slide.Whisky();
			initialSlide.MessageCenter = "Let's play!";
			_machine.ShowSlide(initialSlide);
		}

		[NotNull]
		public async Task AdvanceSlide()
		{
			var nextSlide = await Slide.BurningCar();
			nextSlide.MessageTop = "You are so advanced!";
			_machine.ShowSlide(nextSlide);
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
			var brains = new _MachineBrains(machine);
			machine.Pause.BindTo(brains.Pause);
			machine.AdvanceSlide.BindTo(brains.AdvanceSlide);
			machine.Stop.BindTo(brains.Stop);
			machine.Start.BindTo(brains.Start);
		}
	}
}