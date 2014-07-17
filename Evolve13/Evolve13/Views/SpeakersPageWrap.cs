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
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(5,Device.OnPlatform(20,0,0),5,0),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};

			var speakers = App.Database.GetSpeakers ();

			foreach (var speaker in speakers) {
				var cell = new StackLayout {
					WidthRequest = 50,
					HeightRequest = 50,
					Children = {
						new Image {Source = speaker.HeadshotUrl, 
							VerticalOptions = LayoutOptions.Start,
							WidthRequest=30,
							HeightRequest=30},
						new Label {Text = speaker.Name, 
							Font = Font.SystemFontOfSize(9),
							LineBreakMode = LineBreakMode.TailTruncation,
							VerticalOptions = LayoutOptions.Start, 
							HorizontalOptions = LayoutOptions.Center}
					}
					
				};
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

