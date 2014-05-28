using System;
using Xamarin.QuickUI.Platform.iOS;
using Xamarin.QuickUI;
using MonoTouch.Twitter;

namespace QuickEvolve13.iOS
{
	public class Twitter : ITwitter
	{
		public Twitter ()
		{
		}

		public void Tweet (string tweetText)
		{
			var tweet = new TWTweetComposeViewController();
			tweet.SetInitialText (tweetText);
//			viewController.PresentModalViewController(tweet, true);
		}
	}
}

