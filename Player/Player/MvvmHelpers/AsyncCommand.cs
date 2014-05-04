// Presentation Karaoke Player
// File: AsyncCommand.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;

namespace Player.MvvmHelpers
{
	public class AsyncCommand : HasTarget<Func<Task>>, ICommand
	{
		[NotNull] private Func<Task> _whenCalled;

		public AsyncCommand([NotNull] Func<Task> whenCalled)
		{
			_whenCalled = whenCalled;
		}

		public Func<Task> Target
		{
			get { return _whenCalled; }
		}

		public bool CanExecute([CanBeNull] object parameter)
		{
			return true;
		}

		public void Execute([CanBeNull] object ignored)
		{
			_whenCalled();
		}

		public event EventHandler CanExecuteChanged;

		[NotNull]
		public Task Execute()
		{
			return _whenCalled();
		}

		public void BindTo([NotNull] Func<Task> whenCalled)
		{
			_whenCalled = whenCalled;
		}

		public void BindToSync([NotNull] Action whenCalled)
		{
			_whenCalled = _Wrap(whenCalled);
		}

		[NotNull]
		private static Func<Task> _Wrap([NotNull] Action whenCalled)
		{
			return () =>
			{
				whenCalled();
				return Task.FromResult(true);
			};
		}

		[NotNull]
		public static AsyncCommand Wrapping([NotNull] Action whenCalled)
		{
			return new AsyncCommand(_Wrap(whenCalled));
		}
	}
}