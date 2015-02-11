using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public class DetailPage : ContentPage
	{
		public DetailPage ()
		{
			BackgroundColor = new Color (0, 0, 1, 0.2);

            var text = "Slide > to see the master / menu";

			if (Device.OS == TargetPlatform.Android) {
				text = @"Click the action bar dots to see the master / menu";
			} else if (Device.OS == TargetPlatform.WinPhone) {
                text = @"Click button \/ to see the master / menu ";
            }

			Content = new StackLayout { 
				HorizontalOptions = LayoutOptions.Center,
				Padding = new Thickness (10, 40, 10, 10),
				Children = {

					new Label { Text = text }
				}
			};
		}
	}
}

