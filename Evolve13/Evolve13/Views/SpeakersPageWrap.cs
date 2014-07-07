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
			Title = "Speakers WrapPanel demo";

			WrapLayout layout = new WrapLayout {
				Spacing = 20,
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(5,Device.OnPlatform(20,0,0),5,0),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};

			for (int i = 0; i < 5; i++) {

				layout.Children.Add(new Label() {
					BackgroundColor = Color.Blue,
					WidthRequest = 75,
					HeightRequest = 75,
					YAlign = TextAlignment.Center,
					XAlign = TextAlignment.Center,
					TextColor = Color.White,
					Text = i.ToString(),
				});
			}

			Content = layout;
		}
	}
}

