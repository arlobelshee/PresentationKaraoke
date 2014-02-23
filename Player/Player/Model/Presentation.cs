using JetBrains.Annotations;

namespace Player.Model
{
	public class Presentation
	{
		[NotNull]
		public Slide CurrentSlide { get; set; }

		public Presentation([NotNull] Slide initialSlide)
		{
			CurrentSlide = initialSlide;
		}
	}
}