using System.IO;
using JetBrains.Annotations;

namespace Player.Model
{
	public interface ImageLoader
	{
		[NotNull]
		Stream Load([NotNull] string name);
	}
}