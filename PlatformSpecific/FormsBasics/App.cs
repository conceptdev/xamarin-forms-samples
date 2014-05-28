using System;
using Xamarin.Forms;

namespace PlatformSpecific
{
	public static class App
	{
		public static Page GetMainPage ()
		{
			var mainNav = new NavigationPage (new TodoListXaml ()); 

			return mainNav;
		}
	}
}