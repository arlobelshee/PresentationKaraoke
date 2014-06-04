// Presentation Karaoke Player
// File: HelperTestExtensions.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Player.Tests.ZZSupport.Tests;
using Player.ViewModels;
using Player.Views;

namespace FluentAssertions
{
	internal static class _HelperTestExtensions
	{
		public static void BeShowingOptions([NotNull] this _TypedObjectAssertions<KaraokeMachine> subject)
		{
			subject.TypedSubject.CurrentPageType.Should()
				.Be(typeof (PresentationOptionsPage));
		}

		[NotNull]
		public static _TypedObjectAssertions<KaraokeMachine> Should([NotNull] this KaraokeMachine subject)
		{
			return new _TypedObjectAssertions<KaraokeMachine>(subject);
		}

		[NotNull]
		public static async Task ShouldNotBeEmpty([NotNull] this Stream subject)
		{
			subject.CanRead.Should()
				.BeTrue();
			var buffer = new byte[10];
			var bytesRead = subject.ReadAsync(buffer, 0, 1);
			(await bytesRead).Should()
				.Be(1);
		}
	}
}