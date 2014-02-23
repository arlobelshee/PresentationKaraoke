// Presentation Karaoke Player
// File: App.xaml.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using JetBrains.Annotations;
using Player.Model;

namespace Player
{
	/// <summary>
	///    Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	sealed partial class _App
	{
		[NotNull] private readonly KaraokeMachine _machine;

		/// <summary>
		///    Initializes the singleton application object.  This is the first line of authored code
		///    executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public _App()
		{
			InitializeComponent();
			Suspending += _OnSuspending;
			_machine = KaraokeMachine.BoundToModel();
		}

		/// <summary>
		///    Invoked when the application is launched normally by the end user.  Other entry points
		///    will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="e">Details about the launch request and process.</param>
		protected override void OnLaunched([NotNull] LaunchActivatedEventArgs e)
		{
			_TurnOnDebuggingConsole();
			var rootFrame = _CreateMainWindow();
			_InitializeViewState(e);
			rootFrame.ChangeToCurrentPageIfNotCurrentlyShowingAnything(e);
			_ActivateApplication();
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
		private _RootWindow _CreateMainWindow()
		{
			return _RootWindow.WrapExistingFrameIfPresent(_machine) ?? _RootWindow.InitializeNewWindow(_machine);
		}

		private static void _InitializeViewState([NotNull] LaunchActivatedEventArgs e)
		{
			if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
			{
				_LoadSavedStateFromTombstone();
			}
		}

		private static void _ActivateApplication()
		{
			Window.Current.Activate();
		}

		private static void _SaveApplicationState()
		{
		}

		private static void _LoadSavedStateFromTombstone()
		{
		}

		private void _StopBackgroundActivity()
		{
			_machine.Pause();
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