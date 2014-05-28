using System;
using Xamarin.Forms;

namespace FormsBasics
{
	public class MyStackLayout : ContentPage
	{
		public MyStackLayout ()
		{
			var red = new Label {
				Text = "Stop", 
				BackgroundColor = Color.Red,
				Font = Font.SystemFontOfSize (20),
				WidthRequest = 100
			};
			var yellow = new Label {
				Text = "Slow down", 
				BackgroundColor = Color.Yellow,
				Font = Font.SystemFontOfSize (20),
				WidthRequest = 100
			};
			var green = new Label {
				Text = "Go", 
				BackgroundColor = Color.Green,
				Font = Font.SystemFontOfSize (20),
				WidthRequest = 400
			};

			Content = new StackLayout {
				Spacing = 10,
				VerticalOptions = LayoutOptions.End,
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Start,
				Children = {
					red, yellow, green
				}
			};
		}
	}
}

