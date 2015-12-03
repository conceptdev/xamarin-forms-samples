using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

			MessagingCenter.Subscribe<RestaurantGuide.App, string> (this, "show", async (sender, arg) => {
				// do something whenever the "Hi" message is sent
				Debug.WriteLine("Search argument: " + arg);

				var restaurant = from r in restaurants
						where r.Name == arg
						select r;

				// set initial state
				await MainPage.Navigation.PopToRootAsync ();

				// load screen

				var rPage = new RestaurantDetail ();
				// set BindingContext
				rPage.BindingContext = restaurant.FirstOrDefault();

				// display screen
				await MainPage.Navigation.PushAsync (rPage);
			});
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

		protected override async void OnResume()
		{
			var rid = Application.Current.Properties ["rid"];
			// Handle when your app resumes
			Debug.WriteLine ("OnResume:" + Application.Current.Properties ["rid"]);


		}
	}
}
