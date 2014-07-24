using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	internal interface _SlideLoader : IDisposable
	{
		[NotNull]
		Task<_SlideLibrary> LoadAllSlides();
	}
}