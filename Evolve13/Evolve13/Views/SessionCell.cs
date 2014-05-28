using System;
using Xamarin.Forms;

namespace Evolve13
{
	public class SessionCell : ViewCell
	{
		public SessionCell ()
		{
			var title = new Label {
				YAlign = TextAlignment.Center
			};
			title.SetBinding (Label.TextProperty, "Title");

			var label = new Label {
				YAlign = TextAlignment.Center,
				Font = Font.SystemFontOfSize(10)
			};
			label.SetBinding (Label.TextProperty, "LocationDisplay");

			var fav = new Image {
				Source = FileImageSource.FromFile ("favorite.png"),
			};
			//TODO: implement favorites
			//fav.SetBinding (Image.IsVisibleProperty, "IsFavorite");

			var text = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(0, 0, 0, 0),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {title, label}
			};

			var layout = new StackLayout {
				Padding = new Thickness(20, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {text, fav}
			};
			View = layout;
		}
	}
}

