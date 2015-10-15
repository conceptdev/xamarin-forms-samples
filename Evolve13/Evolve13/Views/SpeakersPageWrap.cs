using System;
using Xamarin.Forms;

/*

NOTE: this is for future implementation - it's currently here just as a demo

*/
namespace Evolve13
{
	public class SpeakersPageWrap : ContentPage
	{
		public SpeakersPageWrap ()
		{
			Title = "Speakers (WrapPanel demo)";

			WrapLayout layout = new WrapLayout {
				Spacing = 5,
				Padding = new Thickness(5,Device.OnPlatform(20,0,0),5,0),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};

			var speakers = App.Database.GetSpeakers ();

			foreach (var speaker in speakers) {

				// configure cell for wrap
				var cell = new StackLayout {
					WidthRequest = 50,
					HeightRequest = 50,
					BackgroundColor = Color.FromRgb(222, 222, 222),
					Children = {
						new Image {Source = speaker.HeadshotUrl, 
							VerticalOptions = LayoutOptions.Start,
							//BackgroundColor = Color.Blue,
							WidthRequest=30,
							HeightRequest=30},
						new Label {Text = speaker.Name, 
							FontSize = 9,
							LineBreakMode = LineBreakMode.TailTruncation,
							VerticalOptions = LayoutOptions.Start, 
							HorizontalOptions = LayoutOptions.Center}
					}
				};

				// add touch handling to show next page
				var tapGestureRecognizer = new TapGestureRecognizer();
				tapGestureRecognizer.CommandParameter = speaker;
				tapGestureRecognizer.Tapped += (sender, e) => {
					var speakr = ((TappedEventArgs)e).Parameter as Speaker;
					var sp = new SpeakerPage();
					sp.BindingContext = speakr;
					Navigation.PushAsync(sp);
				};
				cell.GestureRecognizers.Add(tapGestureRecognizer);

				// add to wrap layout
				layout.Children.Add (cell);
			}

			// simple WrapLayout population
//			for (int i = 0; i < 5; i++) {
//
//				layout.Children.Add(new Label() {
//					BackgroundColor = Color.Blue,
//					WidthRequest = 75,
//					HeightRequest = 75,
//					YAlign = TextAlignment.Center,
//					XAlign = TextAlignment.Center,
//					TextColor = Color.White,
//					Text = i.ToString(),
//				});
//			}


			Content = new ScrollView {
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Content = layout
			};
		}
	}
}

