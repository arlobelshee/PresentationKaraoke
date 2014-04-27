// Presentation Karaoke Player
// File: CommandAssertions.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using FluentAssertions;
using FluentAssertions.Primitives;
using JetBrains.Annotations;
using Player.Model;

namespace Player.Tests.ZZSupport.Tests
{
	public class CommandAssertions : ObjectAssertions
	{
		public CommandAssertions([NotNull] Command testSubject) : base(testSubject.Target)
		{
		}

		[NotNull]
		public AndConstraint<CommandAssertions> BeBoundTo([NotNull] Action expected)
		{
			Be(expected);
			return new AndConstraint<CommandAssertions>(this);
		}
	}
}