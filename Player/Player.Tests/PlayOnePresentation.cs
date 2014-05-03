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
		public void NewlyCreatedMachines_Should_BeShowingAPresentation()
		{
			var testSubject = KaraokeMachine.WithABrain();
			testSubject.CurrentPageType.Should()
				.Be(typeof (PresentationPlayingPage));
			testSubject.CurrentSlide.Should()
				.NotBeNull();
		}
	}
}