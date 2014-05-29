using Android.Speech.Tts;
using Xamarin.Forms;
using System.Collections.Generic;
using TodoMvvm;

[assembly: Dependency (typeof (Speech))]

namespace TodoMvvm
{
	public class Speech : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
	{
		TextToSpeech speaker;
		string toSpeak;
		public Speech ()
		{
		}

		public void Speak (string text)
		{
			var c = Forms.Context; 
			toSpeak = text;
			if (speaker == null) {
				speaker = new TextToSpeech (c, this);
			} else {
				var p = new Dictionary<string,string> ();
				speaker.Speak (toSpeak, QueueMode.Flush, p);
				System.Diagnostics.Debug.WriteLine ("spoke " + toSpeak);
			}
		}

		#region IOnInitListener implementation
		public void OnInit (OperationResult status)
		{
			if (status.Equals (OperationResult.Success)) {
				System.Diagnostics.Debug.WriteLine ("speaker init");
				var p = new Dictionary<string,string> ();
				speaker.Speak (toSpeak, QueueMode.Flush, p);
			} else {
				System.Diagnostics.Debug.WriteLine ("was quiet");
			}
		}
		#endregion
	}
}
