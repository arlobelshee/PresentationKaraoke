using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

namespace Player.Model
{
	internal class _ImageLoaderHardCoded : ImageLoader
	{
		private readonly Dictionary<string, Stream> _images = new Dictionary<string, Stream>();

		public Stream LoadImageData(string name)
		{
			return _images[name];
		}

		public void Add([NotNull] string name, [NotNull] Stream data)
		{
			_images[name] = data;
		}
	}
}