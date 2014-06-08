// Presentation Karaoke Player
// File: SlideLibrary.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _SlideLibrary
	{
		private readonly ImageLoader _images;
		private readonly UiControlMaker _uiControls;
		private readonly Slide[] _allSlides;
		private int _lastSlideGiven;

		public _SlideLibrary([NotNull] IEnumerable<Slide> slides, [NotNull] ImageLoader images,
			[NotNull] UiControlMaker uiControls)
		{
			_images = images;
			_uiControls = uiControls;
			_allSlides = slides.ToArray();
			_lastSlideGiven = _allSlides.Length;
		}

		public int Length
		{
			get { return _allSlides.Length; }
		}

		[NotNull]
		public Task<Slide> PickOneRandomSlide()
		{
			_lastSlideGiven++;
			if (_lastSlideGiven >= _allSlides.Length) _lastSlideGiven = 0;
			return _InflateBackgroundImage(_allSlides[_lastSlideGiven]);
		}

		[NotNull]
		private async Task<Slide> _InflateBackgroundImage([NotNull] Slide nextSlide)
		{
			using (var data = _images.LoadImageData(nextSlide.BackgroundImageName ?? string.Empty))
			{
				nextSlide.Background =
					await _uiControls.CreateImage(data.AsRandomAccessStream());
			}
			return nextSlide;
		}
	}
}