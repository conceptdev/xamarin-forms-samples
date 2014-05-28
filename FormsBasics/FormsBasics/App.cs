using System;
using Xamarin.Forms;

namespace FormsBasics
{
	public static class App
	{
		public static Page GetMainPage ()
		{
			//
			//
			// TODO: Uncomment each layout in turn to try out the different samples
			//
			//

			var mainNav = new MyFirstPage ();

//			var mainNav = new NavigationPage (new MyFirstPage ()); 
			// ALSO REMEMBER TO UNCOMMENT MyFirstPage.cs LINE 26 ==> button.Clicked += (s, e) => Navigation.Push(new MySecondPage());

//			var mainNav = new MyStackLayout ();

//			var mainNav = new MyAbsoluteLayout ();

//			var mainNav = new MyControls ();

			return mainNav;
		}
	}
}

