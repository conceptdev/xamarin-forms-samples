using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public class DetailPage : ContentPage
	{
		public DetailPage ()
		{
			BackgroundColor = new Color (0, 0, 1, 0.2);


			Content = new StackLayout { 
				HorizontalOptions = LayoutOptions.Center,
				Padding = new Thickness (10, 40, 10, 10),
				Children = {

					new Label {Text = "Slide > to see the master / menu"}
				}
			};
		}
	}
}

