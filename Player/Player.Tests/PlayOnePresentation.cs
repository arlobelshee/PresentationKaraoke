using FluentAssertions;
using NUnit.Framework;
using Player.Model;

namespace Player.Tests
{
	[TestFixture]
	public class PlayOnePresentation
	{
		[Test]
		public void APresentationHasOneCurrentSlide()
		{
			var startingSlide = new _Slide();
			var testSubject = new _Presentation(startingSlide);
			testSubject.Slide.Should()
				.Be(startingSlide);
		}
	}
}