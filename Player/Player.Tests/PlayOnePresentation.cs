// Presentation Karaoke Player
// File: PlayOnePresentation.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using FluentAssertions;
using NUnit.Framework;
using Player.ViewModels;
using Player.Views;

namespace Player.Tests
{
	[TestFixture]
	public class PlayOnePresentation
	{
		[Test]
		public void NewlyCreatedMachines_Should_OfferToStartAPresentation()
		{
			var testSubject = KaraokeMachine.WithABrain();
			testSubject.Should()
				.BeShowingOptions();
			testSubject.SlideAdvanceSpeed.Should()
				.Be(20);
		}

		[Test]
		public void StartingAPresentation_Should_ShowSomeSlide()
		{
			var testSubject = KaraokeMachine.WithABrain();
			testSubject.Start.Call();
			testSubject.CurrentPageType.Should()
				.Be(typeof (PresentationPlayingPage));
			testSubject.CurrentSlide.Should()
				.NotBeNull();
		}

		[Test]
		public void StoppingAPresentation_Should_ReturnToOptions()
		{
			var testSubject = KaraokeMachine.WithABrain();
			testSubject.Start.Call();
			testSubject.Stop.Call();
			testSubject.Should()
				.BeShowingOptions();
		}
	}
}