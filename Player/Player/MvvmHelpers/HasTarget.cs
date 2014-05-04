// Presentation Karaoke Player
// File: HasTarget.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using JetBrains.Annotations;

namespace Player.MvvmHelpers
{
	public interface HasTarget<out T>
	{
		[NotNull]
		T Target { get; }
	}
}