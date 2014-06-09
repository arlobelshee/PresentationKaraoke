// Presentation Karaoke Player
// File: _ImageLoaderHardCoded.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.Collections.Generic;
using System.IO;
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

		public Stream Load(string name)
		{
			return _images[name];
		}
	}
}