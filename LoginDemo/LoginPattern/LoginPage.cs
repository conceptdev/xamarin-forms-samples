using System;
using Xamarin.Forms;

namespace LoginPattern
{


	public class LoginPage : ContentPage
	{
		public LoginPage (ILoginManager ilm)
		{
			var button = new Button { Text = "Login" };
			button.Clicked += (sender, e) => {
				ilm.ShowMainPage();
			};
			Content = new StackLayout {
				Padding = new Thickness (10, 40, 10, 10),
				Children = {
					new Label { Text = "Username" },
					new Entry { Text = "" },
					new Label { Text = "Password" },
					new Entry { Text = "" },
					button
				}
			};
		}
	}
}

