// Presentation Karaoke Player
// File: _PresentationData.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Player.ViewModels;

namespace Player.Model
{
	internal class _PresentationData
	{
		// ReSharper disable InconsistentNaming
		[NotNull]
		public _SlideData[] slides { get; set; }

		internal class _SlideData
		{
			[CanBeNull]
			public string top { get; set; }

			[CanBeNull]
			public string middle { get; set; }

			[CanBeNull]
			public string bottom { get; set; }

			[CanBeNull]
			public string background_color { get; set; }

			[CanBeNull]
			public string image_stretch { get; set; }

			[CanBeNull]
			public string text_color { get; set; }

			[NotNull]
			public Slide ToSlide()
			{
				var result = new Slide
				{
					MessageTop = top,
					MessageCenter = middle,
					MessageBottom = bottom,
					BackgroundColor = ColorScheme.FromHtmlArgbStringValue(background_color ?? "#FF000000"),
					BackgroundFill = (Stretch) Enum.Parse(typeof (Stretch), image_stretch ?? "Uniform")
				};
				if (text_color == "white")
				{
					result.UseWhiteText();
				}
				else
				{
					result.UseBlackText();
				}
				return result;
			}
		}

		// ReSharper restore InconsistentNaming
		[NotNull]
		public static Task<_PresentationData> FromJson([NotNull] Stream stream)
		{
			return Task.Factory.StartNew(() =>
			{
				using (var contents = new StreamReader(stream))
				{
					using (var json = new JsonTextReader(contents))
					{
						var reader = new JsonSerializer();
						return reader.Deserialize<_PresentationData>(json);
					}
				}
			});
		}
	}
}