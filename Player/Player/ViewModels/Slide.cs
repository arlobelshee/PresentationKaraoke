// Presentation Karaoke Player
// File: Slide.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using JetBrains.Annotations;
using Player.Model;

namespace Player.ViewModels
{
	public class Slide : FirePropertyChanged
	{
		private string _background;

		[NotNull]
		public string Background
		{
			get { return _background; }
			set
			{
				if (value == _background) return;
				_background = value;
				NotifyChangeWatchers();
			}
		}
	}
}