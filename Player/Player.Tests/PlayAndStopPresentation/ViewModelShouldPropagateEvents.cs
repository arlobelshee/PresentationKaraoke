// Presentation Karaoke Player
// File: ViewModelShouldPropagateEvents.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

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
	public class ViewModelShouldPropagateEvents
	{
		[NotNull] private KaraokeMachine _testSubject;

		[Test]
		public void NormalMachineInitialization_Should_BindEventsToBrains()
		{
			var brains = _MachineBrains.WithTrivialSlidesAndUi(out _testSubject, new StoppedClock());
			_testSubject.Pause.Should()
				.BeBoundTo(brains.Pause);
			_testSubject.AdvanceSlide.Should()
				.BeBoundTo(brains.AdvanceSlide);
			_testSubject.Stop.Should()
				.BeBoundTo(brains.Stop);
			_testSubject.Start.Should()
				.BeBoundTo(brains.Start);
			_testSubject.StartAutoplay.Should()
				.BeBoundTo(brains.StartAutoplay);
			_testSubject.TurnOn.Should()
				.BeBoundTo(brains.PrepareDeck);
		}

		[NotNull,Test]
		public async Task ShowingASlide_Should_SendPropertyChangeNotifications()
		{
			_testSubject.CurrentPageType = typeof (object);
			_testSubject.MonitorEvents();

			_testSubject.ShowSlide(new Slide(new _ImageLoaderHardCoded()));

			_testSubject.ShouldRaisePropertyChangeFor(m => m.CurrentPageType);
			_testSubject.ShouldRaisePropertyChangeFor(m => m.CurrentSlide);
			_testSubject.CurrentPageType.Should()
				.Be(typeof (PresentationPlayingPage));
		}

		[Test]
		public void ShowingOptions_Should_SendPropertyChangeNotifications()
		{
			_testSubject.CurrentPageType = typeof (object);
			_testSubject.MonitorEvents();

			_testSubject.ShowOptions();

			_testSubject.ShouldRaisePropertyChangeFor(m => m.CurrentPageType);
			_testSubject.CurrentPageType.Should()
				.Be(typeof (PresentationOptionsPage));
		}

		[Test]
		public void ChangingSlideSpeed_Should_SendPropertyChangeNotifications()
		{
			_testSubject.MonitorEvents();
			_testSubject.SlideAdvanceSpeed = 99;
			_testSubject.ShouldRaisePropertyChangeFor(m => m.SlideAdvanceSpeed);
		}

		[SetUp]
		public void SetUp()
		{
			_testSubject = KaraokeMachine.Brainless();
		}
	}
}