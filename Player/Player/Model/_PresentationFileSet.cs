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
	internal class _PresentationFileSet
	{
		[NotNull]
		public async Task<_SlideLibrary> ReadPresentation([NotNull] Stream presentationFile)
		{
			var archive = new ZipArchive(presentationFile, ZipArchiveMode.Read);
			var imageData = new _ImageLoaderZip(archive);
			try
			{
				var allSlides = ParseManifest(_LoadIndex(archive), imageData);
				return new _SlideLibrary(await allSlides, imageData);
			}
			catch (Exception)
			{
				imageData.Dispose();
				throw;
			}
		}

		[NotNull]
		private static ZipArchiveEntry _LoadIndex([NotNull] ZipArchive archive)
		{
			var index = archive.GetEntry("index.json");
			if (index == null)
			{
				throw new FormatException("Error in presentation file. I could not find index.json. Please fix the file.");
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
	}
}