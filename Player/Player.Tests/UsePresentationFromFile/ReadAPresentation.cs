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
		private const string ImageName = "image.png";

		[NotNull]
		[Test]
		public async Task ShouldReadOneSlidePresentationWithoutImages()
		{
			using (var zipData = new MemoryStream())
			{
				await _WriteTrivialOneSlidePresoToStream(zipData);
				var testSubject = new _PresentationFileSet();
				var preso = await testSubject.ReadPresentation(zipData, UiControlMaker.Simulated());
				_ShouldBeTrivialOneSlidePreso(preso);
			}
		}

		[Test]
		public void ShouldBeAbleToAccessBuiltInImageDataFromTests()
		{
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
			var expectedSlide = new Slide
			{
				BackgroundColor = Color.FromArgb(1, 2, 3, 4),
				MessageBottom = "bottom",
				MessageTop = "top",
				MessageCenter = "middle",
				BackgroundFill = Stretch.Fill,
				BackgroundImageName = "img.png"
			};
			expectedSlide.UseWhiteText();
			testSubject.ToSlide()
				.ShouldBeEquivalentTo(expectedSlide);
		}

		[Test]
		public void SlideDataThatSpecifiesNothingShouldUseDefaults()
		{
			var testSubject = new _PresentationData._SlideData();
			var expectedSlide = new Slide
			{
				BackgroundColor = Color.FromArgb(255, 0, 0, 0),
				MessageBottom = null,
				MessageTop = null,
				MessageCenter = null,
				BackgroundFill = Stretch.Uniform,
				BackgroundImageName = null
			};
			expectedSlide.UseBlackText();
			testSubject.ToSlide()
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
				using (var result = testSubject.Load(ImageName))
				{
					await result.ShouldNotBeEmpty();
				}
			}
		}

		[NotNull,Test]
		public async Task SlideShouldLookUpImageInLoaderOnDemand()
		{
			const string name = "frog";
			var images = new _ImageLoaderHardCoded();
			using (var data = new MemoryStream())
			{
				images.Add(name, data);
				var firstSlide = new Slide
				{
					BackgroundImageName = name
				};
				var secondSlide = new Slide
				{
					BackgroundImageName = name
				};
				var testSubject = new _SlideLibrary(new[] {firstSlide, secondSlide});
				firstSlide.Background.Should()
					.BeNull();
				secondSlide.Background.Should()
					.BeNull();
				var inflatedSlide = await testSubject.PickOneRandomSlide();
				inflatedSlide.Background.Should()
					.NotBeNull();
				var nextSlide = inflatedSlide;
				while (nextSlide == inflatedSlide)
				{
					nextSlide = await testSubject.PickOneRandomSlide();
				}
				inflatedSlide.Background.Should()
					.BeNull();
			}
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

		private static void _ShouldBeTrivialOneSlidePreso([NotNull] _SlideLibrary preso)
		{
			preso.Length.Should()
				.Be(1);
			var onlySlide = preso.PickOneRandomSlide();
			onlySlide.ShouldBeEquivalentTo(new
			{
				MessageTop = "Hello, world!"
			}, config => config.ExcludingMissingProperties());
		}
	}
}