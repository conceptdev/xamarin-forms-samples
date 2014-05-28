using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Forms2Native
{
	/// <summary>
	/// This is a Xamarin.Forms screen - the first one shown in the app
	/// </summary>
	public class MyFirstPage : ContentPage
	{
		public MyFirstPage ()
		{
			Title = "My First Xamarin.Forms";

			var label = new Label {
				Text = "Hello native rendering...",
				Font = Font.SystemFontOfSize (20),
			};

			var button = new Button { Text = "Click to see a native page" };

			button.Clicked += (s, e) => Navigation.Push(new MySecondPage());

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
	