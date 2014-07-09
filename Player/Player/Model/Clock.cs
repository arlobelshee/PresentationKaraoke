// Presentation Karaoke Player
// File: Clock.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	public abstract class Clock
	{
		[NotNull]
		public RecurringEvent Schedule(TimeSpan frequency, [NotNull] Func<Task> action)
		{
			var whatAndWhen = new RecurringEvent(frequency, action);
			_Schedule(whatAndWhen);
			return whatAndWhen;
		}

		[NotNull]
		protected abstract void _Schedule([NotNull] RecurringEvent whatAndWhen);
	}
}