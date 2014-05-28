using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsBasics
{
	public class MyFirstPage : ContentPage
	{
		public MyFirstPage ()
		{
			Title = "My First Xamarin.Forms";

			var label = new Label {
				Text = "Hello",
				Font = Font.SystemFontOfSize (20),
			};

			var button = new Button { Text = "Click Me!" };

			int i = 1;

			button.Clicked += (s, e) => button.Text = "You clicked me: " + i++;

//			button.Clicked += (s, e) => Navigation.Push(new MySecondPage());

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

