using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public class CreateAccountPage : ContentPage
	{
		public CreateAccountPage (ILoginManager ilm)
		{
			var button = new Button { Text = "Create Account" };
			button.Clicked += (sender, e) => {
				DisplayAlert("Account created", "Add processing login here", "OK");
				ilm.ShowMainPage();
			};
			var cancel = new Button { Text = "Cancel" };
			cancel.Clicked += (sender, e) => {
				MessagingCenter.Send<ContentPage> (this, "Login");
			};
			Content = new StackLayout {
				Padding = new Thickness (10, 40, 10, 10),
				Children = {
					new Label { Text = "Create Account", Font = Font.SystemFontOfSize(NamedSize.Large) }, 
					new Label { Text = "Choose a Username" },
					new Entry { Text = "" },
					new Label { Text = "Password" },
					new Entry { Text = "" },
					new Label { Text = "Re-enter Password" },
					new Entry { Text = "" },
					button, cancel
				}
			};
		}
	}
}

