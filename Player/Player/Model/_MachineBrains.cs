﻿// Presentation Karaoke Player
// File: _MachineBrains.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _MachineBrains
	{
		[NotNull] private readonly KaraokeMachine _machine;

		public _MachineBrains([NotNull] KaraokeMachine machine)
		{
			_machine = machine;
			machine.Pause.BindTo(Pause);
			machine.AdvanceSlide.BindTo(AdvanceSlide);
			machine.Brains_TestAccess = this;
		}

		public void BeginPresentation()
		{
			var initialSlide = Slide.BurningCar();
			initialSlide.MessageCenter = "Let's play!";
			_machine.ShowSlide(initialSlide);
		}

		public void Pause() { }

		public void AdvanceSlide()
		{
			var nextSlide = Slide.BurningCar();
			nextSlide.MessageTop = "You are so advanced!";
			_machine.ShowSlide(nextSlide);
		}
	}
}