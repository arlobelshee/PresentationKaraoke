// Presentation Karaoke Player
// File: CommandAssertions.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using FluentAssertions;
using FluentAssertions.Primitives;
using JetBrains.Annotations;
using Player.Model;

namespace Player.Tests.ZZSupport.Tests
{
	public class CommandAssertions<T, TCommand> : ObjectAssertions where TCommand : HasTarget<T>
	{
		public CommandAssertions([NotNull] TCommand testSubject)
			: base(testSubject.Target)
		{
		}

		[NotNull]
		public AndConstraint<CommandAssertions<T, TCommand>> BeBoundTo([NotNull] T expected)
		{
			Be(expected);
			return new AndConstraint<CommandAssertions<T, TCommand>>(this);
		}
	}
}