// Presentation Karaoke Player
// File: _KaraokeMachine.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using JetBrains.Annotations;

namespace Player.Model
{
	internal class _KaraokeMachine
	{
		[NotNull]
		public Type CurrentPageType
		{
			get { return typeof (MainPage); }
		}

		[NotNull]
		public Action Pause;

		[NotNull]
		public static _KaraokeMachine BoundToModel()
		{
			return new _KaraokeMachine();
		}

		public _KaraokeMachine()
		{
			Pause = _NoOp;
		}

		private static void _NoOp()
		{
		}
	}
}