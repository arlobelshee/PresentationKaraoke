// Presentation Karaoke Player
// File: BeAWinStoreApplication.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using FluentAssertions;
using NUnit.Framework;
using Player.Model;

namespace Player.Tests
{
	[TestFixture]
	public class BeAWinStoreApplication
	{
		[Test]
		public void NormalMachineInitialization_Should_BindEventsToBrains()
		{
			var testSubject = _KaraokeMachine.BoundToModel();
			testSubject.Pause.Should()
				.NotBeNull();
		}
	}
}