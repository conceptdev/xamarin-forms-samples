using System;
using Xamarin.Forms;

namespace DialogPro
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			return new NavigationPage(new SettingsPage ());
		}
	}
}

