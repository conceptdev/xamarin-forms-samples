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
			var startingRestId = " (not set)";

			if (Application.Current.Properties.ContainsKey("rid")){
				var o = Application.Current.Properties ["rid"];
				// contains the value (eg. 5)
				var s = Application.Current.Properties ["rid"] as string;
				// contains null (used to contain "5")
				//var t = (string)Application.Current.Properties ["rid"];
				// throws an invalid cast exception!

				startingRestId = o.ToString ();
				if (!String.IsNullOrWhiteSpace (startingRestId)) {
					var rPage = new RestaurantDetail ();
					// set BindingContext
					rPage.BindingContext = restaurants [Convert.ToInt32 (o)];
					MainPage.Navigation.PushAsync (rPage);
				} else {
					startingRestId = "(set but not valid)";
				}
				
			}
			Debug.WriteLine ("OnStart:" + startingRestId);
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
