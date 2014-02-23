// Presentation Karaoke Player
// File: _MachineBrains.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using JetBrains.Annotations;

namespace Player.Model
{
	internal class _MachineBrains
	{
		[NotNull] private readonly KaraokeMachine _machine;

		public _MachineBrains([NotNull] KaraokeMachine machine)
		{
			_machine = machine;
		}

		public void BeginPresentation()
		{
			_machine.NowPlaying = new Presentation(new Slide());
			_machine.CurrentPageType = typeof (PresentationPlayingPage);
		}
	}
}