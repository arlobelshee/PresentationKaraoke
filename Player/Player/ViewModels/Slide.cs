// Presentation Karaoke Player
// File: Slide.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using JetBrains.Annotations;

namespace Player.ViewModels
{
	public class Slide
	{
		[NotNull]
		public string Background { get; set; }

		[CanBeNull]
		public string MessageTop { get; set; }

		[CanBeNull]
		public string MessageCenter { get; set; }

		[CanBeNull]
		public string MessageBottom { get; set; }

		public static Slide BurningCar()
		{
			var initialSlide = new Slide
			{
				Background = "../Assets/burning_car.jpeg"
			};
			return initialSlide;
		}
	}
}