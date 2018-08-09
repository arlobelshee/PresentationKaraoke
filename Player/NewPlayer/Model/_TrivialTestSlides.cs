using System.Threading.Tasks;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal static class _TrivialTestSlides
	{
		[NotNull]
		public static Task<_SlideLibrary> LoadAllSlides()
		{
			var noImages = new _ImageLoaderHardCoded();
			var slide1 = new Slide(noImages)
			{
				MessageTop = "first slide"
			};
			slide1.UseBlackText();
			var slide2 = new Slide(noImages)
			{
				MessageTop = "second slide"
			};
			slide2.UseWhiteText();
			return Task.FromResult(new _SlideLibrary(new[] { slide1, slide2 }, noImages));
		}
	}
}