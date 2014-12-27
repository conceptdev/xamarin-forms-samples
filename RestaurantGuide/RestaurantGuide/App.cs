using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace RestaurantGuide
{
	public class App : Application
	{
		static List<Restaurant> restaurants;

		public static void SetContent (List<Restaurant> r)
		{
			restaurants = r;
		}

		public App ()
		{	
			MainPage = new NavigationPage(new RestaurantList (restaurants));
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			var r = "";
			if (Application.Current.Properties.ContainsKey("rid")){
				r = Application.Current.Properties ["rid"] as string;
				if (!String.IsNullOrWhiteSpace(r)) {
					var rPage = new RestaurantDetail();
					// set BindingContext
					MainPage.Navigation.PushAsync(rPage);
				}
				
			}
			Debug.WriteLine ("OnStart:" + r);
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
			Debug.WriteLine ("OnSleep:" + Application.Current.Properties ["rid"]);
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
			Debug.WriteLine ("OnResume:" + Application.Current.Properties ["rid"]);
		}
	}
}
