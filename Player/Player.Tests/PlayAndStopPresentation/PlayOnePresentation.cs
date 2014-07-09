// Presentation Karaoke Player
// File: PlayOnePresentation.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using NUnit.Framework;
using Player.Model;
using Player.ViewModels;
using Player.Views;

namespace Player.Tests.PlayAndStopPresentation
{
	[TestFixture]
	public class PlayOnePresentation
	{
		[NotNull] private KaraokeMachine _machine;
		[NotNull] private _MachineBrains _testSubject;
		[NotNull] private StoppedClock _clock;

		[Test]
		public void NewlyCreatedMachines_Should_OfferToStartAPresentation()
		{
			_machine.Should()
				.BeShowingOptions();
			_machine.SlideAdvanceSpeed.Should()
				.Be(20);
		}

		[NotNull]
		[Test]
		public async Task StartingAPresentation_Should_ShowSomeSlide()
		{
			await _testSubject.Start();
			_machine.CurrentPageType.Should()
				.Be(typeof (PresentationPlayingPage));
			_machine.CurrentSlide.Should()
				.NotBeNull();
		}

		[NotNull]
		[Test]
		public async Task StartingAnAutoplayPresentation_Should_SetUpSchedule()
		{
			_machine.SlideAdvanceSpeed = 33;
			await _testSubject.StartAutoplay();
			_clock.Triggers.Single()
				.Should()
				.Be(new RecurringEvent(TimeSpan.FromSeconds(33), _testSubject.AdvanceSlide));
		}


		[NotNull]
		[Test]
		public async Task StoppingAnAutoplayPresentation_Should_CancelScheduleAndShowOptions()
		{
			_machine.SlideAdvanceSpeed = 33;
			await _testSubject.StartAutoplay();
			_testSubject.Stop();
			_clock.Triggers.Should()
				.BeEmpty();
			_machine.Should()
				.BeShowingOptions();
		}

		[NotNull]
		[Test]
		public async Task StoppingAPresentation_Should_ReturnToOptions()
		{
			await _testSubject.Start();
			_testSubject.Stop();
			_machine.Should()
				.BeShowingOptions();
		}

		[SetUp]
		public void SetUp()
		{
			_clock = new StoppedClock();
			_testSubject = _MachineBrains.WithTrivialSlidesAndUi(out _machine, _clock);
		}
	}
}