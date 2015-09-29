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
			// HACK: not sure why, this awful/unnecessary hack call seems to force the userInfo to be preserved for AppDelegate.ContinueUserActivity
			w.UpdateUserActivityState (UserActivityHelper.CreateNSUserActivity (restaurant));
			// end hack https://forums.developer.apple.com/thread/9690
		}
		public void Stop() {
			var w = RestaurantGuide.iOS.AppDelegate.Current.window;
			Console.WriteLine (" UserActivity.ResignCurrent ");
			w.UserActivity.ResignCurrent ();
		}
	}
}

