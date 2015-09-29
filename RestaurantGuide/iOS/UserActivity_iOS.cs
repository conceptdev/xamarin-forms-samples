using System;
using Xamarin.Forms;
using RestaurantGuide.iOS;

[assembly: Dependency (typeof (UserActivity_iOS))]

namespace RestaurantGuide.iOS
{
	public class UserActivity_iOS : IUserActivity
	{
		public void Start(Restaurant restaurant) {
			var w = RestaurantGuide.iOS.AppDelegate.Current.window;
			Console.WriteLine ($" UserActivity.BecomeCurrent ({restaurant.Name})");
			w.UserActivity = UserActivityHelper.CreateNSUserActivity (restaurant);
		}
		public void Stop() {
			var w = RestaurantGuide.iOS.AppDelegate.Current.window;
			Console.WriteLine (" UserActivity.ResignCurrent ");
			w.UserActivity.ResignCurrent ();
		}
	}
}

