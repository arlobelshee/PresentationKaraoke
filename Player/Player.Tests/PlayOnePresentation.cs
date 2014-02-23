// Presentation Karaoke Player
// File: PlayOnePresentation.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using FluentAssertions;
using NUnit.Framework;
using Player.Model;

namespace Player.Tests
{
	[TestFixture]
	public class PlayOnePresentation
	{
		[Test]
		public void APresentationHasOneCurrentSlide()
		{
			var startingSlide = new Slide();
			var testSubject = new Presentation(startingSlide);
			testSubject.CurrentSlide.Should()
				.Be(startingSlide);
		}

		[Test]
		public void TellingTheMachineToPlay_Should_UpdateThePresentationAndStartPlayingIt()
		{
			var machine = new KaraokeMachine();
			var testSubject = new _MachineBrains(machine);
			machine.MonitorEvents();
			testSubject.BeginPresentation();
			machine.ShouldRaisePropertyChangeFor(m => m.CurrentPageType);
			machine.ShouldRaisePropertyChangeFor(m => m.NowPlaying);
			machine.CurrentPageType.Should()
				.Be(typeof (PresentationPlayingPage));
			machine.NowPlaying.Should()
				.NotBeNull();
		}

		[Test]
		public void NewlyCreatedMachines_Should_BeShowingAPresentation()
		{
			var testSubject = KaraokeMachine.BoundToModel();
			testSubject.CurrentPageType.Should()
				.Be(typeof (PresentationPlayingPage));
			testSubject.NowPlaying.Should()
				.NotBeNull();
		}
	}
}