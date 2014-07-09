// Presentation Karaoke Player
// File: FirePropertyChanged.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Player.Model;

namespace Player.MvvmHelpers
{
	public class FirePropertyChanged : INotifyPropertyChanged
	{
		[NotNull] private readonly ExecuteVia _notificationThread;

		public FirePropertyChanged([NotNull] ExecuteVia notificationThread)
		{
			_notificationThread = notificationThread;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyChangeWatchers([NotNull] [CallerMemberName] string changedProperty = null)
		{
			_notificationThread
				.Do(() => _NotifyListeners(changedProperty));
		}

		private void _NotifyListeners([NotNull] string changedProperty)
		{
			if (PropertyChanged == null) return;
			PropertyChanged(this, new PropertyChangedEventArgs(changedProperty));
		}
	}
}