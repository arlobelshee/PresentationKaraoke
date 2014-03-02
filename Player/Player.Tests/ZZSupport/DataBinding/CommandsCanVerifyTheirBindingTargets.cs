// Presentation Karaoke Player
// File: CommandsCanVerifyTheirBindingTargets.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using FluentAssertions.Primitives;
using JetBrains.Annotations;
using NUnit.Framework;
using Player.Tests.ZZSupport.Tests;

namespace Player.Tests.ZZSupport.DataBinding
{
	[TestFixture]
	public class CommandsCanVerifyTheirBindingTargets
	{
		[Test]
		public void NewCommand_Should_BeBoundToItsTarget()
		{
			var testSubject = new Command(_RecordCalls);
			testSubject.Should()
				.BeBoundTo(_RecordCalls);
		}

		private void _RecordCalls()
		{
		}
	}

	public class CommandAssertions : ObjectAssertions
	{
		public CommandAssertions([NotNull] Command testSubject) : base(testSubject)
		{
		}

		public void BeBoundTo(Action expected)
		{
			throw new NotImplementedException();
		}
	}

	public class Command
	{
		private readonly Action _whenCalled;

		public Command(Action whenCalled)
		{
			_whenCalled = whenCalled;
		}
	}
}