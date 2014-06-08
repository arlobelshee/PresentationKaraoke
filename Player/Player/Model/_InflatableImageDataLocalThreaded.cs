// Presentation Karaoke Player
// File: _InflatableImageDataLocalThreaded.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	internal class _InflatableImageDataLocalThreaded : InflatableImageData
	{
		[CanBeNull]
		public Stream Data { get; private set; }

		public _InflatableImageDataLocalThreaded([NotNull] string name, [NotNull] ImageLoader imageData)
			: base(name, imageData)
		{
		}

		public override void Deflate()
		{
			if (Data == null) return;
			Data.Dispose();
			Data = null;
		}

		protected override Task _InflateWith(Stream data)
		{
			Deflate();
			Data = data;
			return Task.FromResult(true);
		}
	}
}