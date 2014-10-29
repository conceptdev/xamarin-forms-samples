using System;
using Xamarin.Forms;
using System.Diagnostics;
using MonoTouch.AVFoundation;
using BugSweeper.iOS;
using MonoTouch.Foundation;


[assembly: Xamarin.Forms.Dependency (typeof (PlaySound_iOS))]

namespace BugSweeper.iOS
{
	public class PlaySound_iOS : IPlaySound
	{
		#region IPlaySound implementation

		public void PlayLaugh ()
		{
			// http://xamapp.com/playing-sound-with-xamarin-ios/
			var soundurl = NSUrl.FromFilename("laugh.mp3"); // http://soundbible.com/2055-Evil-Laugh-Male-6.html
			var onePlay = AVAudioPlayer.FromUrl(soundurl);
			//var onePlay = player[playIndex];
			onePlay.CurrentTime = 4;// 4 secs; onePlay.Duration*3;
			onePlay.NumberOfLoops = 1;
			onePlay.Volume = 1.0f;
//			onePlay.FinishedPlaying += DidFinishPlaying;
			onePlay.PrepareToPlay();
			onePlay.Play();
		}

		#endregion
	}
}

