using System.IO;
using System.IO.Compression;
using JetBrains.Annotations;

namespace Player.Model
{
	internal class _ImageLoader
	{
		private readonly ZipArchive _imageBundle;

		public _ImageLoader([NotNull] ZipArchive imageBundle)
		{
			_imageBundle = imageBundle;
		}

		[NotNull]
		public Stream LoadImageData([NotNull] string name)
		{
			return _imageBundle.GetEntry(name)
				.Open();
		}
	}
}