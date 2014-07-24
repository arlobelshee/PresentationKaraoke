// Presentation Karaoke Player
// File: _PresentationFileSet.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _PresentationFileSet : IDisposable
	{
		[NotNull] private readonly ZipArchive _archive;

		public _PresentationFileSet([NotNull] Stream presentationFile)
		{
			_archive = new ZipArchive(presentationFile, ZipArchiveMode.Read);
		}

		[NotNull]
		public async Task<_SlideLibrary> ReadPresentation()
		{
			var allSlides = ParseManifest(_LoadIndex(), new _ImageLoaderZip(_archive));
			return new _SlideLibrary(await allSlides);
		}

		[NotNull]
		private ZipArchiveEntry _LoadIndex()
		{
			var index = _archive.GetEntry("index.json");
			if (index == null)
			{
				throw new FormatException("This presentation is incomplete. I could not find index.json.");
			}
			return index;
		}

		[NotNull]
		private async Task<IEnumerable<Slide>> ParseManifest([NotNull] ZipArchiveEntry manifest, [NotNull] ImageLoader imageData)
		{
			_PresentationData presentation;
			using (var stream = manifest.Open())
			{
				presentation = await _PresentationData.FromJson(stream);
			}
			return presentation.slides.Select(s => s.ToSlide(imageData));
		}

		public void Dispose()
		{
			_archive.Dispose();
		}
	}
}