// Presentation Karaoke Player
// File: ImageLoader.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	public interface ImageLoader
	{
		[NotNull]
		Task<Stream> Load([NotNull] string name);
	}
}