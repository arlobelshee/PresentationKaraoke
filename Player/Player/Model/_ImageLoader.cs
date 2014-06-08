using System.IO;
using JetBrains.Annotations;

namespace Player.Model
{
	public interface ImageLoader
	{
		[NotNull]
		Stream LoadImageData([NotNull] string name);
	}
}