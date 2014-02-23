// Presentation Karaoke Player
// File: FirePropertyChanged.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Player.Model
{
	public class FirePropertyChanged : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyChangeWatchers([CanBeNull,CallerMemberName] string changedProperty=null)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(changedProperty));
		}
	}
}