// Presentation Karaoke Player
// File: Command.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using JetBrains.Annotations;

namespace Player.Model
{
	public class Command
	{
		[NotNull] private Action _whenCalled;

		public Command([NotNull] Action whenCalled)
		{
			_whenCalled = whenCalled;
		}

		[NotNull]
		public Action Target
		{
			get { return _whenCalled; }
		}

		public void Call()
		{
			_whenCalled();
		}

		public void BindTo(Action whenCalled)
		{
			_whenCalled = whenCalled;
		}
	}
}