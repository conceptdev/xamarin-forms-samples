using System;
using Xamarin.Forms;

namespace PlatformSpecific
{
	public static class App
	{
		public static Page GetMainPage ()
		{
			var mainNav = new NavigationPage (new TodoListXaml ()); 

			mainNav = Device.OnPlatform<NavigationPage> (
				iOS:      new NavigationPage (new TodoListXaml ()),
				Android:  new NavigationPage (new TodoListXaml ()),
				WinPhone: new NavigationPage (new TodoListXaml ())
			);

			return mainNav;
		}
	}
}