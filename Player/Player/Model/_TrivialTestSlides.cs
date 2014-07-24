// Presentation Karaoke Player
// File: _TrivialTestSlides.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.Threading.Tasks;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _TrivialTestSlides : _SlideLoader
	{
		[NotNull] private readonly Slide _slide1;
		[NotNull] private readonly Slide _slide2;

		public _TrivialTestSlides()
		{
			var noImages = new _ImageLoaderHardCoded();
			_slide1 = new Slide(noImages)
			{
				MessageTop = "first slide"
			};
			_slide1.UseBlackText();
			_slide2 = new Slide(noImages)
			{
				MessageTop = "second slide"
			};
			_slide2.UseWhiteText();
		}

		public Task<_SlideLibrary> LoadAllSlides()
		{
			return Task.FromResult(new _SlideLibrary(new[] {_slide1, _slide2}));
		}

		public void Dispose()
		{
		}
	}
}