using Windows.UI.Xaml.Media;
using JetBrains.Annotations;

namespace Player.Model
{
	public class Slide : FirePropertyChanged
	{
		private string _background;

		[NotNull]
		public string Background
		{
			get { return _background; }
			set
			{
				if (value == _background) return;
				_background = value;
				NotifyChangeWatchers();
			}
		}
	}
}