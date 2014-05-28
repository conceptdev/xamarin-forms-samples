using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace RestaurantGuide
{
	public class App
	{
		static List<Restaurant> restaurants;

		public static void SetContent (List<Restaurant> r)
		{
			restaurants = r;
		}

		public static Page GetMainPage ()
		{	
			return new NavigationPage(new RestaurantList (restaurants));
		}
	}
}

