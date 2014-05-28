using System;
using Xamarin.Forms;

namespace FormsBasics
{
public class MySecondPage : ContentPage
{
	public MySecondPage ()
	{
		var label = new Label {
			Text = "This is the second page",
			Font = Font.SystemFontOfSize (36),
		};

		Content = new StackLayout {
			Spacing = 30,
			VerticalOptions = LayoutOptions.Start,
			Children = {
				label,
			}
		};
	}
}
}

