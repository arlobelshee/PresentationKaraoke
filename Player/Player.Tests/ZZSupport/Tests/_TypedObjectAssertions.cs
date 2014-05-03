using FluentAssertions.Primitives;

namespace Player.Tests.ZZSupport.Tests
{
	internal class _TypedObjectAssertions<T> : ObjectAssertions
	{
		protected internal _TypedObjectAssertions(T value) : base(value)
		{
		}

		public T TypedSubject
		{
			get { return (T) Subject; }
		}
	}
}