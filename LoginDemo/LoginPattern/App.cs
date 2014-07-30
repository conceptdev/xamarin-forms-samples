using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public static class App
	{
		static ILoginManager loginManager;

		public static Page GetLoginPage (ILoginManager ilm)
		{	
			loginManager = ilm;
//			return new LoginPage (ilm);
			return new LoginModalPage (ilm);
		}

		public static Page GetMainPage ()
		{	
			return new MainPage ();
		}

		public static void Logout ()
		{
			loginManager.Logout();
		}
	}
}

