// Presentation Karaoke Player
// File: SlideLibrary.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _SlideLibrary
	{
		private readonly Slide[] _allSlides;
		private int _lastSlideGiven;

		public _SlideLibrary([NotNull] Slide[] slides)
		{
			_allSlides = slides;
			_lastSlideGiven = slides.Length;
		}

		[NotNull]
		public Slide PickOneRandomSlide()
		{
			_lastSlideGiven++;
			if (_lastSlideGiven >= _allSlides.Length) _lastSlideGiven = 0;
			return _allSlides[_lastSlideGiven];
		}
	}
}