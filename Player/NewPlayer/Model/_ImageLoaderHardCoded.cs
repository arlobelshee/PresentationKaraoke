// Presentation Karaoke Player
// File: _ImageLoaderHardCoded.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	internal class _ImageLoaderHardCoded : ImageLoader
	{
		[NotNull] private readonly Dictionary<string, Stream> _images = new Dictionary<string, Stream>();

		public void Add([NotNull] string name, [NotNull] Stream data)
		{
			_images[name] = data;
		}

		public async Task<Stream> Load(string name)
		{
			var result = new MemoryStream();
			var src = _images[name];
			src.Seek(0, SeekOrigin.Begin);
			await src.CopyToAsync(result);
			await result.FlushAsync();
			result.Seek(0, SeekOrigin.Begin);
			return result;
		}

		public void Dispose()
		{
			foreach (var stream in _images.Values)
			{
				stream.Dispose();
			}
			_images.Clear();
		}
	}
}