// Presentation Karaoke Player
// File: _PresentationData.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.IO;
using System.Threading.Tasks;
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

			[NotNull]
			public Slide ToSlide()
			{
				return new Slide
				{
					MessageTop = top
				};
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