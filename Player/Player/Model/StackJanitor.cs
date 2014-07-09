using System;
using JetBrains.Annotations;

namespace Player.Model
{
	public class StackJanitor : IDisposable
	{
		[CanBeNull] private Action _undo;

		public StackJanitor([NotNull] Action undo)
		{
			_undo = undo;
		}

		public void Dispose()
		{
			if (_undo != null) _undo();
		}

		public void Commit()
		{
			_undo = null;
		}
	}
}