// Presentation Karaoke Player
// File: Command.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Windows.Input;
using JetBrains.Annotations;

namespace Player.MvvmHelpers
{
	public class Command : HasTarget<Action>, ICommand
	{
		[NotNull] private Action _whenCalled;

		public Command([NotNull] Action whenCalled)
		{
			_whenCalled = whenCalled;
		}

		public Action Target => _whenCalled;

		public bool CanExecute([CanBeNull] object parameter)
		{
			return true;
		}

		public void Execute([CanBeNull] object ignored=null)
		{
			_whenCalled();
		}

		public event EventHandler CanExecuteChanged;

		public void BindTo([NotNull] Action whenCalled)
		{
			_whenCalled = whenCalled;
		}
	}
}