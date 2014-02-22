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
			var testSubject = new _Presentation(new _Slide());
		}
	}
}