// Presentation Karaoke Player
// File: CommandTestExtensions.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using JetBrains.Annotations;
using Player.Tests.ZZSupport.DataBinding;

namespace Player.Tests.ZZSupport.Tests
{
	public static class CommandTestExtensions
	{
		[NotNull]
		public static CommandAssertions Should([NotNull] this Command testSubject)
		{
			return new CommandAssertions(testSubject);
		}
	}
}