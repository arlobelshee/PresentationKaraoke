// Presentation Karaoke Player
// File: _WallClock.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading;

namespace Player.Model
{
	internal class _WallClock : Clock
	{
		protected override void _Schedule(RecurringEvent whatAndWhen)
		{
			whatAndWhen.Cancelable = new Timer(_ => whatAndWhen.Action().RunSynchronously(), null, TimeSpan.Zero,
				whatAndWhen.Frequency);
		}
	}
}