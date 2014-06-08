// Presentation Karaoke Player
// File: InflatableImageDataUiThreaded.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using JetBrains.Annotations;

namespace Player.Model
{
	public class InflatableImageDataUiThreaded : InflatableImageData
	{
		private readonly TaskFactory _uiThreadTaskFactory;

		public InflatableImageDataUiThreaded([NotNull] string name, [NotNull] ImageLoader imageData, [NotNull] TaskFactory uiThreadTaskFactory) : base(name, imageData)
		{
			_uiThreadTaskFactory = uiThreadTaskFactory;
		}

		[CanBeNull]
		public BitmapImage Image { get; private set; }

		public override void Deflate()
		{
			Image = null;
		}

		protected override Task _InflateWith(Stream data)
		{
			return _uiThreadTaskFactory.StartNew(async () =>
			{
				Image = new BitmapImage();
				await Image.SetSourceAsync(data.AsRandomAccessStream());
			}).Unwrap();
		}
	}
}