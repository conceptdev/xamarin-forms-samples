using System;
using Xamarin.Forms;

namespace QuickTodo
{
	public class MyRelativeLayout : ContentPage
	{
		public MyRelativeLayout ()
		{
			var red = new Label {
				Text = "Stop", 
				BackgroundColor = Color.Red,
				Font = Font.SystemFontOfSize (20), 
				WidthRequest = 200, HeightRequest = 30
			};
			var yellow = new Label {
				Text = "Slow down", 
				BackgroundColor = Color.Yellow,
				Font = Font.SystemFontOfSize (20),
				WidthRequest = 160, HeightRequest = 160
			};
			var green = new Label {
				Text = "Go", 
				BackgroundColor = Color.Green,
				Font = Font.SystemFontOfSize (20),
				WidthRequest = 50, HeightRequest = 50
			};

			var relLayout = new RelativeLayout (); 
//			relLayout.Children.Add (red, new Point (20, 20));
//			relLayout.Children.Add (yellow, new Point (40, 60));
//			relLayout.Children.Add (green, new Point (80, 180));
//
			Content = relLayout;
		}
	}
}

