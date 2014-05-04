using System.Threading.Tasks;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal static class _TrivialTestSlides
	{
		[NotNull] private static readonly Slide Slide1;
		[NotNull] private static readonly Slide Slide2;

		static _TrivialTestSlides()
		{
			Slide1 = new Slide
			{
				MessageTop = "first slide"
			};
			Slide1.UseBlackText();
			Slide2 = new Slide
			{
				MessageTop = "second slide"
			};
			Slide2.UseWhiteText();
		}

		[NotNull]
		public static Task<_SlideLibrary> LoadAllSlides()
		{
			return Task.FromResult(new _SlideLibrary(new[] {Slide1, Slide2}));
		}
	}
}