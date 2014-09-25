using System;
using Xamarin.Forms;

namespace Todo
{
	public class TodoItemCell : ViewCell
	{
		public TodoItemCell ()
		{
			var label = new Label {
				YAlign = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Font = Constants.Font
			};
			label.SetBinding (Label.TextProperty, "Name");
			if (Device.OS != TargetPlatform.iOS) { // WinPhone & Android
				label.TextColor = Color.Black;
			}

			var tick = new Image {
				Source = FileImageSource.FromFile ("check1.png"),
				HorizontalOptions = LayoutOptions.EndAndExpand
			};
			tick.SetBinding (Image.IsVisibleProperty, "Done");

			var layout = new StackLayout {
				Padding = new Thickness(10, 0, 10, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {label, tick},

			};

			var lineLayout = new StackLayout {
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					layout, 
					new BoxView {
						HeightRequest = 1,
						WidthRequest = 600,
						VerticalOptions = LayoutOptions.End,
						Color = Constants.Brown,
						Opacity = 0.5
					}
				},
				BackgroundColor = Color.FromRgb (255,244,165)
			};
			View = lineLayout;
		}
	}
}

