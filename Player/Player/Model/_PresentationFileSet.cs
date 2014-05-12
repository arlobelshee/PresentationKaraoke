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
using Newtonsoft.Json;
using Player.ViewModels;

namespace Player.Model
{
	internal class _PresentationFileSet
	{
		[NotNull]
		public async Task<_SlideLibrary> ReadPresentation([NotNull] Stream presentationFile)
		{
			var archive = new ZipArchive(presentationFile, ZipArchiveMode.Read);
			var manifest = archive.GetEntry("index.json");
			var allSlides = ParseManifest(manifest);
			return new _SlideLibrary(await allSlides);
		}

		[NotNull]
		private async Task<IEnumerable<Slide>> ParseManifest([NotNull] ZipArchiveEntry manifest)
		{
			var presentation = await ParseJson(manifest);
			return presentation.slides.Select(s => new Slide
			{
				MessageTop = s.top
			});
		}

		[NotNull]
		private Task<_PresentationData> ParseJson([NotNull] ZipArchiveEntry manifest)
		{
			return Task.Factory.StartNew(() =>
			{
				using (var stream = manifest.Open())
				{
					using (var contents = new StreamReader(stream))
					{
						using (var json = new JsonTextReader(contents))
						{
							var reader = new JsonSerializer();
							return reader.Deserialize<_PresentationData>(json);
						}
					}
				}
			});
		}
	}
}