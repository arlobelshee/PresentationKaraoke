// Presentation Karaoke Player
// File: StoppedClock.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace Player.Model
{
	public class StoppedClock : Clock
	{
		[NotNull] private readonly List<RecurringEvent> _triggers;

		public StoppedClock()
		{
			_triggers = new List<RecurringEvent>();
		}

		[NotNull]
		public IEnumerable<RecurringEvent> Triggers
		{
			get { return _triggers; }
		}

		protected override void _Schedule(RecurringEvent whatAndWhen)
		{
			_triggers.Add(whatAndWhen);
			whatAndWhen.Cancelable = new StackJanitor(() => _triggers.Remove(whatAndWhen));
		}
	}
}