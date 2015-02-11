using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public class App : Application, ILoginManager
	{
		static ILoginManager loginManager;
		public static App Current;

		public App ()
		{	
			Current = this;

			var isLoggedIn = Properties.ContainsKey("IsLoggedIn")?(bool)Properties ["IsLoggedIn"]:false;

			// we remember if they're logged in, and only display the login page if they're not
			if (isLoggedIn)
				MainPage = new LoginPattern.MainPage ();
			else
				MainPage = new LoginModalPage (this);
		}

		public void ShowMainPage ()
		{	
			MainPage = new MainPage ();
		}

		public void Logout ()
		{
			Properties ["IsLoggedIn"] = false; // only gets set to 'true' on the LoginPage
			MainPage = new LoginModalPage (this);
		}
	}
}

