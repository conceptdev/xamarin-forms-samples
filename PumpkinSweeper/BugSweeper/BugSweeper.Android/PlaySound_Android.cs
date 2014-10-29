using System;
using Xamarin.Forms;
using Android.Media;
using BugSweeper.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (PlaySound_Android))]

namespace BugSweeper.Droid
{
	public class PlaySound_Android : IPlaySound
	{
		#region IPlaySound implementation

		public void PlayLaugh ()
		{
			// http://developer.xamarin.com/recipes/android/media/audio/play_audio/
			MediaPlayer _player;
			_player = MediaPlayer.Create(Forms.Context, Resource.Raw.laugh); // http://soundbible.com/2055-Evil-Laugh-Male-6.html
			_player.Start ();
		}

		#endregion
	}
}

