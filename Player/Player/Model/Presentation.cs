using JetBrains.Annotations;

namespace Player.Model
{
	internal class _Presentation
	{
		[NotNull]
		public _Slide Slide { get; set; }

		public _Presentation([NotNull] _Slide slide)
		{
			Slide = slide;
		}
	}
}