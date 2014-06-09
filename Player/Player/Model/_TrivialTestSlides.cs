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
			var noImages = new _ImageLoaderHardCoded();
			Slide1 = new Slide(noImages)
			{
				MessageTop = "first slide"
			};
			Slide1.UseBlackText();
			Slide2 = new Slide(noImages)
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