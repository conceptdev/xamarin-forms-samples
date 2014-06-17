using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace Evolve13
{
	public class SpeakerPage : ContentPage
	{
		TweetButton tweetButton;

		public SpeakerPage ()
		{
			Title = "Speaker";

			NavigationPage.SetHasNavigationBar (this, true);


			var name = new Label { 
				Text = "Name",
				Font = Font.SystemFontOfSize(18)
			};
			name.SetBinding (Label.TextProperty, "Name");

			// Twitter is iOS only for now (see Speaker class for binding calculation)
			tweetButton = new TweetButton {
//				BackgroundImage = "tweet.png", 
				Text = "Tweet",
			};
			tweetButton.SetBinding (Button.IsVisibleProperty, "HasTwitter");

			var bio = new Label { 
				Text = "Bio",
				Font = Font.SystemFontOfSize(12)
			};
			bio.SetBinding (Label.TextProperty, "Bio");

			var scroll = new ScrollView { 
				Orientation = ScrollOrientation.Vertical,
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.StartAndExpand,
					Padding = new Thickness(20),
					Children = { name, tweetButton, bio }
				}
			};
			Content = scroll;
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			var speaker = BindingContext as Speaker; 
			tweetButton.Tweet = speaker.TwitterHandle + " " + TweetButton.TwitterHashtag;
			Debug.WriteLine ("Tweet: "+tweetButton.Tweet);
		}
	}
}