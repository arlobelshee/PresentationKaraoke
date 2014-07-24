// Presentation Karaoke Player
// File: ReadAPresentation.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using FluentAssertions;
using JetBrains.Annotations;
using NUnit.Framework;
using Player.Model;
using Player.ViewModels;

namespace Player.Tests.UsePresentationFromFile
{
	[TestFixture]
	public class ReadAPresentation
	{
		private _ImageLoaderHardCoded _noImages;
		private const string ImageName = "image.png";

		[NotNull]
		[Test]
		public async Task ShouldReadOneSlidePresentationWithoutImages()
		{
			using (var zipData = new MemoryStream())
			{
				await _WriteTrivialOneSlidePresoToStream(zipData);
				_SlideLibrary preso;
				using (var testSubject = new _PresentationFileSet(zipData))
				{
					preso = await testSubject.LoadAllSlides();
				}
				preso.Length.Should()
					.Be(1);
				var onlySlide = preso.PickOneRandomSlide();
				onlySlide.ShouldBeEquivalentTo(new
				{
					MessageTop = "Hello, world!"
				}, config => config.ExcludingMissingProperties());
			}
		}

		[Test]
		public void ShouldBeAbleToAccessBuiltInImageDataFromTests()
		{
			using (var builtInSlides = new _BuiltInSlides())
			using (var data = _BuiltInSlides.ImageDataFor(_BuiltInSlides.WhiskeyFileName))
			{
				data.Length.Should()
					.BeGreaterThan(0);
			}
		}

		[Test]
		public void SlideDataThatSpecifiesEverythingShouldInflateCorrectly()
		{
			var testSubject = new _PresentationData._SlideData
			{
				bottom = "bottom",
				middle = "middle",
				top = "top",
				background_color = "#01020304",
				image_stretch = "Fill",
				text_color = "white",
				background_image = "img.png"
			};
			var expectedSlide = new Slide(_noImages)
			{
				BackgroundColor = Color.FromArgb(1, 2, 3, 4),
				MessageBottom = "bottom",
				MessageTop = "top",
				MessageCenter = "middle",
				BackgroundFill = Stretch.Fill,
				BackgroundImageName = "img.png"
			};
			expectedSlide.UseWhiteText();
			testSubject.ToSlide(_noImages)
				.ShouldBeEquivalentTo(expectedSlide);
		}

		[Test]
		public void SlideDataThatSpecifiesNothingShouldUseDefaults()
		{
			var testSubject = new _PresentationData._SlideData();
			var expectedSlide = new Slide(_noImages)
			{
				BackgroundColor = Color.FromArgb(255, 0, 0, 0),
				MessageBottom = null,
				MessageTop = null,
				MessageCenter = null,
				BackgroundFill = Stretch.Uniform,
				BackgroundImageName = null
			};
			expectedSlide.UseBlackText();
			testSubject.ToSlide(_noImages)
				.ShouldBeEquivalentTo(expectedSlide);
		}

		[NotNull]
		[Test]
		public async Task ImageLoaderShouldLoadImagesFromArchive()
		{
			using (var zipData = new MemoryStream())
			{
				await _WriteImageToStream(zipData);
				var testSubject = new _ImageLoaderZip(new ZipArchive(zipData, ZipArchiveMode.Read));
				using (var result = await testSubject.Load(ImageName))
				{
					await result.ShouldNotBeEmpty();
				}
			}
		}

		[SetUp]
		public void SetUp()
		{
			_noImages = new _ImageLoaderHardCoded();
		}

		[NotNull]
		private static async Task _WriteTrivialOneSlidePresoToStream([NotNull] Stream zipData)
		{
			using (var file = new ZipArchive(zipData, ZipArchiveMode.Create, true))
			{
				await _WriteManifest(file);
			}
		}

		[NotNull]
		private static async Task _WriteImageToStream([NotNull] Stream zipData)
		{
			using (var file = new ZipArchive(zipData, ZipArchiveMode.Create, true))
			{
				await _WriteImage(file);
			}
		}

		[NotNull]
		private static async Task _WriteImage([NotNull] ZipArchive file)
		{
			var manifest = file.CreateEntry(ImageName);
			using (var manifestContents = manifest.Open())
			{
				await _BuiltInSlides.CopyArbitraryImageToStream(manifestContents);
			}
		}

		[NotNull]
		private static async Task _WriteManifest([NotNull] ZipArchive file)
		{
			var manifest = file.CreateEntry("index.json");
			using (var manifestContents = new StreamWriter(manifest.Open()))
			{
				await manifestContents.WriteAsync(@"{""slides"": [{""top"":""Hello, world!""}]}");
			}
		}
	}
}