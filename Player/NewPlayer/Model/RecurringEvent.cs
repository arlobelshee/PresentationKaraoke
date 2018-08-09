// Presentation Karaoke Player
// File: RecurringEvent.cs
// 
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Player.Model
{
	public class RecurringEvent : IEquatable<RecurringEvent>, IDisposable
	{
		private readonly TimeSpan _frequency;
		[NotNull] private readonly Func<Task> _action;
		[CanBeNull] private IDisposable _cancelable;

		public RecurringEvent(TimeSpan frequency, [NotNull] Func<Task> action)
		{
			_frequency = frequency;
			_action = action;
			_cancelable = null;
		}

		[NotNull]
		public Func<Task> Action
		{
			get { return _action; }
		}

		public TimeSpan Frequency
		{
			get { return _frequency; }
		}

		[NotNull]
		public IDisposable Cancelable
		{
			set { _cancelable = value; }
		}

		public bool Equals([CanBeNull] RecurringEvent other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Frequency.Equals(other.Frequency) && Action.Equals(other.Action);
		}

		public override bool Equals([CanBeNull] object obj)
		{
			return Equals(obj as RecurringEvent);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Frequency.GetHashCode()*397) ^ Action.GetHashCode();
			}
		}

		public static bool operator ==(RecurringEvent left, RecurringEvent right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(RecurringEvent left, RecurringEvent right)
		{
			return !Equals(left, right);
		}

		[NotNull]
		public override string ToString()
		{
			return string.Format("Frequency: {0}, Action: {1}", Frequency, Action);
		}

		public void Dispose()
		{
			if (_cancelable != null) _cancelable.Dispose();
		}
	}
}