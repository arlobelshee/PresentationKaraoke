// Presentation Karaoke Player
// File: CommandsCanVerifyTheirBindingTargets.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using FluentAssertions;
using NUnit.Framework;
using Player.MvvmHelpers;

namespace Player.Tests.ZZSupport.DataBinding
{
	[TestFixture]
	public class CommandsCanVerifyTheirBindingTargets
	{
		private int _calls;

		[Test]
		public void NewCommand_Should_BeBoundToItsTarget()
		{
			var testSubject = new Command(_RecordCalls);
			testSubject.Should()
				.BeBoundTo(_RecordCalls);
		}

		[Test]
		public void NewCommand_Should_CallItsTarget()
		{
			var testSubject = new Command(_RecordCalls);
			_calls.Should()
				.Be(0);
			testSubject.Execute();
			_calls.Should()
				.Be(1);
		}

		[Test]
		[ExpectedException(MatchType = MessageMatch.Contains,
			ExpectedMessage = "Expected object to be \r\n\r\nSystem.Action\r\n{\r\n   Method = Void _RecordCalls()")]
		public void BeBoundTo_Should_ReportErrorsCorrectly()
		{
			var testSubject = new Command(() => { });
			testSubject.Should()
				.BeBoundTo(_RecordCalls);
		}

		[SetUp]
		public void SetUp()
		{
			_calls = 0;
		}

		private void _RecordCalls()
		{
			_calls++;
		}
	}
}