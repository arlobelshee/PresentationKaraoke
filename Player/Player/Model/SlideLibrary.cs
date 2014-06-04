// Presentation Karaoke Player
// File: SlideLibrary.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _SlideLibrary
	{
		private readonly Slide[] _allSlides;
		private int _lastSlideGiven;

		public _SlideLibrary([NotNull] IEnumerable<Slide> slides, _ImageLoader imageLoader)
		{
			_allSlides = slides.ToArray();
			_lastSlideGiven = _allSlides.Length;
		}

		public int Length
		{
			get { return _allSlides.Length; }
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