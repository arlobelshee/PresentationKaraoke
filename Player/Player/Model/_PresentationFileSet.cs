// Presentation Karaoke Player
// File: _PresentationFileSet.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _PresentationFileSet
	{
		[NotNull]
		public async Task<_SlideLibrary> ReadPresentation([NotNull] Stream presentationFile)
		{
			var archive = new ZipArchive(presentationFile, ZipArchiveMode.Read);
			var allSlides = ParseManifest(archive.GetEntry("index.json"));
			return new _SlideLibrary(await allSlides, new _ImageLoader(archive));
		}

		[NotNull]
		private async Task<IEnumerable<Slide>> ParseManifest([NotNull] ZipArchiveEntry manifest)
		{
			_PresentationData presentation;
			using (var stream = manifest.Open())
			{
				presentation = await _PresentationData.FromJson(stream);
			}
			return presentation.slides.Select(s => s.ToSlide());
		}
	}
}