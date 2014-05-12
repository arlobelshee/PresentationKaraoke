// Presentation Karaoke Player
// File: ReadAPresentation.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using NUnit.Framework;
using Player.Model;

namespace Player.Tests.UsePresentationFromFile
{
	[TestFixture]
	public class ReadAPresentation
	{
		[NotNull]
		[Test]
		public async Task ShouldReadOneSlidePresentationWithoutImages()
		{
			using (var zipData = new MemoryStream())
			{
				await _WriteTrivialOneSlidePresoToStream(zipData);
				var testSubject = new _PresentationFileSet();
				var preso = await testSubject.ReadPresentation(zipData);
				preso.ShouldBeEquivalentTo(new
				{
					Length = 1
				}, config => config.ExcludingMissingProperties());
				var onlySlide = preso.PickOneRandomSlide();
				onlySlide.ShouldBeEquivalentTo(new
				{
					MessageTop = "Hello, world!"
				}, config => config.ExcludingMissingProperties());
			}
		}

		[NotNull]
		private static async Task _WriteTrivialOneSlidePresoToStream([NotNull] MemoryStream zipData)
		{
			using (var file = new ZipArchive(zipData, ZipArchiveMode.Create, true))
			{
				var manifest = file.CreateEntry("index.json");
				using (var manifestContents = new StreamWriter(manifest.Open()))
				{
					await manifestContents.WriteAsync(@"{""slides"": [{""top"":""Hello, world!""}]}");
				}
			}
		}
	}
}