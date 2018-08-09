// Presentation Karaoke Player
// File: _ImageLoaderZip.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	internal class _ImageLoaderZip : ImageLoader
	{
		[NotNull] private readonly ZipArchive _imageBundle;
		[NotNull] private readonly ExecuteVia _worker = ExecuteVia.BackgroundWorkers();

		public _ImageLoaderZip([NotNull] ZipArchive imageBundle)
		{
			_imageBundle = imageBundle;
		}

		public Task<Stream> Load(string name)
		{
			return _worker.Do(async () =>
			{
				using (var rawZip = _imageBundle.GetEntry(name)
						.Open())
				{
					var result = new MemoryStream();
					await rawZip.CopyToAsync(result);
					result.Seek(0, SeekOrigin.Begin);
					return (Stream) result;
				}
			});
		}

		public void Dispose()
		{
			_imageBundle.Dispose();
		}
	}
}