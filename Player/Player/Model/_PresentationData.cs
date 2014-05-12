// Presentation Karaoke Player
// File: _PresentationData.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using JetBrains.Annotations;

namespace Player.Model
{
	internal class _PresentationData
	{
		// ReSharper disable InconsistentNaming
		[NotNull]
		public _SlideData[] slides { get; set; }

		internal class _SlideData
		{
			[CanBeNull]
			public string top { get; set; }

			[CanBeNull]
			public string middle { get; set; }

			[CanBeNull]
			public string bottom { get; set; }
		}

		// ReSharper restore InconsistentNaming
	}
}