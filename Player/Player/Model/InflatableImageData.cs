// Presentation Karaoke Player
// File: InflatableImageData.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	public abstract class InflatableImageData
	{
		[NotNull] private readonly string _name;
		[NotNull] private readonly ImageLoader _imageData;

		protected InflatableImageData([NotNull] string name, [NotNull] ImageLoader imageData)
		{
			_name = name;
			_imageData = imageData;
		}

		[NotNull]
		public async Task Inflate()
		{
			using (var data = _imageData.Load(_name))
			{
				await _InflateWith(data);
			}
		}

		public abstract void Deflate();

		[NotNull]
		protected abstract Task _InflateWith([NotNull] Stream data);
	}
}