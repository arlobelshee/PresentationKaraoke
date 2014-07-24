// Presentation Karaoke Player
// File: SlideLibrary.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _SlideLibrary : IDisposable
	{
		private readonly ImageLoader _imageData;
		[NotNull] private readonly Slide[] _allSlides;
		private int _lastSlideGiven;
		[NotNull] private readonly Random _rng = new Random();

		public _SlideLibrary([NotNull] IEnumerable<Slide> slides, ImageLoader imageData)
		{
			_imageData = imageData;
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
			var next = _lastSlideGiven;
			while (next == _lastSlideGiven)
			{
				next = _rng.Next(_allSlides.Length);
			}
			_lastSlideGiven = next;
			return _allSlides[_lastSlideGiven];
		}

		public void Dispose()
		{
			_imageData.Dispose();
		}
	}
}