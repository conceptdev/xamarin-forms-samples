using System;

namespace TodoXaml
{
	public class Speech : ITextToSpeech
	{
		public Speech ()
		{
		}

		public void Speak (string text)
		{
			throw new NotImplementedException ("need text-to-speech code for android");
			// http://conceptdev.blogspot.com/2013/09/android-texttospeech-api-with-xamarin.html
//			var p = new Dictionary ();
//			speaker.Speak (text, QueueMode.Flush, p);		
		}
	}
}