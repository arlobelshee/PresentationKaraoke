// Presentation Karaoke Player
// File: PlayOnePresentation.cs
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
	public class PlayOnePresentation
	{
		private KaraokeMachine _machine;
		private _MachineBrains _testSubject;

		[Test]
		public void NewlyCreatedMachines_Should_OfferToStartAPresentation()
		{
			_machine.Should()
				.BeShowingOptions();
			_machine.SlideAdvanceSpeed.Should()
				.Be(20);
		}

		[NotNull,Test]
		public async Task StartingAPresentation_Should_ShowSomeSlide()
		{
			await _testSubject.Start();
			_machine.CurrentPageType.Should()
				.Be(typeof (PresentationPlayingPage));
			_machine.CurrentSlide.Should()
				.NotBeNull();
		}

		[NotNull,Test]
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
			_testSubject = _MachineBrains.WithTrivialSlidesAndUi(out _machine);
		}
	}
}