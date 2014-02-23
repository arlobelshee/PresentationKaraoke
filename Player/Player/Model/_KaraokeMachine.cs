// Presentation Karaoke Player
// File: _KaraokeMachine.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Player.Model
{
	public class KaraokeMachine : FirePropertyChanged
	{
		[NotNull]
		public Type CurrentPageType
		{
			get { return _currentPageType; }
			set
			{
				if (value == _currentPageType) return;
				_currentPageType = value;
				NotifyChangeWatchers();
			}
		}

		[NotNull]
		public Presentation NowPlaying
		{
			get { return _nowPlaying; }
			set
			{
				if (value == _nowPlaying) return;
				_nowPlaying = value;
				NotifyChangeWatchers();
			}
		}

		[NotNull] public Action Pause;

		private Presentation _nowPlaying;
		private Type _currentPageType;

		[NotNull]
		public static KaraokeMachine BoundToModel()
		{
			return new KaraokeMachine();
		}

		public KaraokeMachine()
		{
			Pause = _NoOp;
		}

		private static void _NoOp()
		{
		}
	}
}