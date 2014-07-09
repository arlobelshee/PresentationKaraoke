// Presentation Karaoke Player
// File: CommandAssertions.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using FluentAssertions;
using FluentAssertions.Primitives;
using JetBrains.Annotations;
using Player.MvvmHelpers;

namespace Player.Tests.ZZSupport.Tests
{
	public class CommandAssertions<T, TCommand> where TCommand : HasTarget<T>
	{
		[NotNull] private readonly ObjectAssertions _helper;

		public CommandAssertions([NotNull] TCommand testSubject)
		{
			_helper = new ObjectAssertions(testSubject.Target);
		}

		[NotNull]
		public AndConstraint<CommandAssertions<T, TCommand>> BeBoundTo([NotNull] T expected)
		{
			_helper.Be(expected);
			return new AndConstraint<CommandAssertions<T, TCommand>>(this);
		}
	}
}