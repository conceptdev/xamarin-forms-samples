using System;
using Xamarin.Forms;

namespace FormsBasics
{
	public class MyControls : ContentPage
	{
		public MyControls ()
		{
			Title = "My First Xamarin.Forms";

			var label = new Label {
				Text = "Custom label",
				Font = Font.SystemFontOfSize (20),
				TextColor = Color.Aqua,
				BackgroundColor = Color.Gray,
				IsVisible = true,
				LineBreakMode = LineBreakMode.WordWrap
			};

			var button = new Button { Text = "Click Me!" };
			int i = 1;
			button.Clicked += (s, e) => button.Text = "You clicked me: " + i++;

			button.Clicked += (s, e) => Navigation.PushAsync(new MySecondPage());

			Content = new StackLayout {
				Spacing = 10,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					label,
					button
				}
			};
		}
	}
}

