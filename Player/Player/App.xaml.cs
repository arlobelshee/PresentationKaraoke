// Presentation Karaoke Player
// File: App.xaml.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using JetBrains.Annotations;

namespace Player
{
	/// <summary>
	///    Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	sealed partial class _App
	{
		/// <summary>
		///    Initializes the singleton application object.  This is the first line of authored code
		///    executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public _App()
		{
			InitializeComponent();
			Suspending += _OnSuspending;
		}

		/// <summary>
		///    Invoked when the application is launched normally by the end user.  Other entry points
		///    will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="e">Details about the launch request and process.</param>
		protected override void OnLaunched([NotNull] LaunchActivatedEventArgs e)
		{
			_TurnOnDebuggingConsole();
			var rootFrame = _CreateMainWindow(e);
			_NavigateToCorrectStartingPage(e, rootFrame);
			_ActivateApplication();
		}

		/// <summary>
		///    Invoked when Navigation to a certain page fails
		/// </summary>
		/// <param name="sender">The Frame which failed navigation</param>
		/// <param name="e">Details about the navigation failure</param>
		private static void _OnNavigationFailed([NotNull] object sender, [NotNull] NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		/// <summary>
		///    Invoked when application execution is being suspended.  Application state is saved
		///    without knowing whether the application will be terminated or resumed with the contents
		///    of memory still intact.
		/// </summary>
		/// <param name="sender">The source of the suspend request.</param>
		/// <param name="e">Details about the suspend request.</param>
		private void _OnSuspending([NotNull] object sender, [NotNull] SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			_StopBackgroundActivity();
			_SaveApplicationState();
			deferral.Complete();
		}

		[NotNull]
		private static Frame _CreateMainWindow([NotNull] LaunchActivatedEventArgs e)
		{
			var rootFrame = Window.Current.Content as Frame;
			if (rootFrame != null) return rootFrame;

			rootFrame = _InitializeRootWindow(e);
			Window.Current.Content = rootFrame;
			return rootFrame;
		}

		[NotNull]
		private static Frame _InitializeRootWindow([NotNull] LaunchActivatedEventArgs e)
		{
			var rootFrame = new Frame {Language = ApplicationLanguages.Languages[0]};
			rootFrame.NavigationFailed += _OnNavigationFailed;

			if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
			{
				_LoadSavedStateFromTombstone();
			}
			return rootFrame;
		}

		private static void _ActivateApplication()
		{
			Window.Current.Activate();
		}

		private static void _NavigateToCorrectStartingPage([NotNull] LaunchActivatedEventArgs e, [NotNull] Frame rootFrame)
		{
			if (rootFrame.Content == null)
			{
				rootFrame.Navigate(typeof (MainPage), e.Arguments);
			}
		}

		private static void _SaveApplicationState()
		{
		}

		private static void _LoadSavedStateFromTombstone()
		{
		}

		private static void _StopBackgroundActivity()
		{
		}

		private void _TurnOnDebuggingConsole()
		{
#if DEBUG
			if (Debugger.IsAttached)
			{
				DebugSettings.EnableFrameRateCounter = true;
			}
#endif
		}
	}
}