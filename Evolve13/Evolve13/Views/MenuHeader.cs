using System;
using Xamarin.Forms;

namespace Evolve13
{
	public class MenuHeader : ViewCell
	{
		public MenuHeader () {

			var label = new Label () {
				Text = "Evolve 2013",
				TextColor = Color.Gray,
				Font = Font.BoldSystemFontOfSize(20)
			};

			Height = 60;

			View = new StackLayout {
				Padding = new Thickness(20),
				BackgroundColor = App.HeaderTint,
				Children = { label }
			};
		}
	}
}