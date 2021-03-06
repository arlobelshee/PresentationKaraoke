﻿// Presentation Karaoke Player
// File: _RootWindow.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using JetBrains.Annotations;
using Player.ViewModels;
using Player.Views;

namespace Player
{
	internal class _RootWindow
	{
		private readonly Frame _rootFrame;
		private readonly KaraokeMachine _machine;

		private _RootWindow([NotNull] Frame rootFrame, [NotNull] KaraokeMachine machine)
		{
			_rootFrame = rootFrame;
			_machine = machine;
			_rootFrame.Focus(FocusState.Programmatic);
			_machine.PropertyChanged +=
				(sender, args) => { if (string.Equals(args.PropertyName, "CurrentPageType")) ChangeToCurrentPage(); };
		}

		[CanBeNull]
		public static _RootWindow WrapExistingFrameIfPresent([NotNull] KaraokeMachine machine)
		{
			var rootFrame = Window.Current.Content as Frame;
			return rootFrame == null ? null : new _RootWindow(rootFrame, machine);
		}

		[NotNull]
		public static _RootWindow InitializeNewWindow([NotNull] KaraokeMachine machine)
		{
			var rootFrame = new Frame
			{
				Language = ApplicationLanguages.Languages[0]
			};
			rootFrame.NavigationFailed += _OnNavigationFailed;
			Window.Current.Content = rootFrame;

			return new _RootWindow(rootFrame, machine);
		}

		public void ChangeToCurrentPageIfNotCurrentlyShowingAnything([NotNull] LaunchActivatedEventArgs e)
		{
			if (_rootFrame.Content == null)
			{
				ChangeToCurrentPage(e.Arguments);
			}
		}

		public void ChangeToCurrentPage([NotNull] string arguments)
		{
			_UnbindOldPageFromMachine();
			_rootFrame.Navigate(_machine.CurrentPageType, arguments);
			_BindNewPageToMachine();
		}

		public void ChangeToCurrentPage()
		{
			_UnbindOldPageFromMachine();
			_rootFrame.Navigate(_machine.CurrentPageType);
			_BindNewPageToMachine();
		}

		private void _BindNewPageToMachine()
		{
			var page = _rootFrame.Content as Page;
			if (page != null)
			{
				page.DataContext = _machine;
			}
			var target = _rootFrame.Content as NavigationTarget;
			if (target != null)
			{
				_rootFrame.KeyUp += target.OnKey;
			}
		}

		private void _UnbindOldPageFromMachine()
		{
			var page = _rootFrame.Content as Page;
			if (page != null)
			{
				page.DataContext = null;
			}
			var target = _rootFrame.Content as NavigationTarget;
			if (target != null)
			{
				_rootFrame.KeyUp -= target.OnKey;
			}
		}

		private static void _OnNavigationFailed([NotNull] object sender, [NotNull] NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}
	}
}