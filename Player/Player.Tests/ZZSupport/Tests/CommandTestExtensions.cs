// Presentation Karaoke Player
// File: CommandTestExtensions.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Player.Model;
using Player.Tests.ZZSupport.Tests;

namespace FluentAssertions
{
	public static class CommandTestExtensions
	{
		[NotNull]
		public static CommandAssertions<Action, Command> Should([NotNull] this Command testSubject)
		{
			return new CommandAssertions<Action, Command>(testSubject);
		}

		[NotNull]
		public static CommandAssertions<Func<Task>, AsyncCommand> Should([NotNull] this AsyncCommand testSubject)
		{
			return new CommandAssertions<Func<Task>, AsyncCommand> (testSubject);
		}
	}
}