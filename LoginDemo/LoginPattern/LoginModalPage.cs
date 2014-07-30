using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public class LoginModalPage : CarouselPage
	{
		ContentPage login, create;
		public LoginModalPage (ILoginManager ilm)
		{
			login = new LoginPage (ilm);
			create = new CreateAccountPage (ilm);
			this.Children.Add (login);
			this.Children.Add (create);

			MessagingCenter.Subscribe<ContentPage> (this, "Login", (sender) => {
				this.SelectedItem = login;
			});
			MessagingCenter.Subscribe<ContentPage> (this, "Create", (sender) => {
				this.SelectedItem = create;
			});
		}
	}
}

