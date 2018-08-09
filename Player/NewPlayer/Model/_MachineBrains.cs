// Presentation Karaoke Player
// File: _MachineBrains.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using JetBrains.Annotations;
using Player.ViewModels;

namespace Player.Model
{
	internal class _MachineBrains : IDisposable
	{
		[NotNull] private readonly KaraokeMachine _machine;
		[NotNull] private readonly Func<Task<_SlideLibrary>> _slideLoader;
		[NotNull] private readonly Clock _clock;
		[CanBeNull] private _SlideLibrary _slideLibrary;
		[CanBeNull] private RecurringEvent _slideAdvancer;
		[NotNull] private Task<Slide> _slideBeingPrepared;

		private _MachineBrains([NotNull] KaraokeMachine machine, [NotNull] Func<Task<_SlideLibrary>> slideLoader,
			[NotNull] Clock clock)
		{
			_machine = machine;
			_slideLoader = slideLoader;
			_clock = clock;
			_machine.ShowOptions();
			_machine.SlideAdvanceSpeed = 10;
			_slideLibrary = null;
		}

		[NotNull]
		public Task Start()
		{
			return AdvanceSlide();
		}

		[NotNull]
		public Task StartAutoplay()
		{
			_slideAdvancer = _clock.Schedule(TimeSpan.FromSeconds(_machine.SlideAdvanceSpeed), AdvanceSlide);
			return AdvanceSlide();
		}

		[NotNull]
		public async Task AdvanceSlide()
		{
			var inflationDone = _StartPreparingOneSlide();
			inflationDone = Interlocked.Exchange(ref _slideBeingPrepared, inflationDone);
			_ChangeSlideBeingDisplayed(await inflationDone);
		}

		public void Pause()
		{
		}

		public void Stop()
		{
			if (_slideAdvancer != null)
			{
				_slideAdvancer.Dispose();
				_slideAdvancer = null;
			}
			_machine.ShowOptions();
		}

		private void _ChangeSlideBeingDisplayed([NotNull] Slide nextSlide)
		{
			var oldSlide = _machine.CurrentSlide;
			_machine.ShowSlide(nextSlide);
			_machine.ControlMaker.Deflate(oldSlide);
		}

		[NotNull]
		private async Task<Slide> _StartPreparingOneSlide()
		{
			var onDeck = _slideLibrary.PickOneRandomSlide();
			return await _machine.ControlMaker.Inflate(onDeck);
		}

		public static void SupplyBrainFor([NotNull] KaraokeMachine machine)
		{
			_ConnectBrainsToMachine(machine, _OpenPresentationFile, new _WallClock());
		}

		[NotNull]
		public async Task PrepareDeck()
		{
			_slideLibrary = await _slideLoader();
			_slideBeingPrepared = _StartPreparingOneSlide();
		}

		[NotNull]
		private static async Task<_SlideLibrary> _OpenPresentationFile()
		{
			StorageFile deck = null;
			while (deck == null)
			{
				var openDiaog = new FileOpenPicker();
				openDiaog.FileTypeFilter.Add(".karaoke");
				deck = await openDiaog.PickSingleFileAsync();
			}
			var contents = deck.OpenReadAsync();
			return await _PresentationFileSet.ReadPresentation((await contents).AsStreamForRead());
		}

		[NotNull]
		public static _MachineBrains WithTrivialSlidesAndUi(out KaraokeMachine machine, [NotNull] Clock clock)
		{
			machine = new KaraokeMachine(ExecuteVia.BackgroundWorkers());
			return _ConnectBrainsToMachine(machine, _TrivialTestSlides.LoadAllSlides, clock);
		}

		[NotNull]
		private static _MachineBrains _ConnectBrainsToMachine([NotNull] KaraokeMachine machine,
			[NotNull] Func<Task<_SlideLibrary>> slideLoader, [NotNull] Clock clock)
		{
			var brains = new _MachineBrains(machine, slideLoader, clock);
			machine.Pause.BindTo(brains.Pause);
			machine.AdvanceSlide.BindTo(brains.AdvanceSlide);
			machine.Stop.BindTo(brains.Stop);
			machine.Start.BindTo(brains.Start);
			machine.StartAutoplay.BindTo(brains.StartAutoplay);
			machine.TurnOn.BindTo(brains.PrepareDeck);
			machine._CleanUp = brains.Dispose;
			return brains;
		}

		public void Dispose()
		{
			if (_slideLibrary != null) _slideLibrary.Dispose();
			_slideLibrary = null;
		}
	}
}