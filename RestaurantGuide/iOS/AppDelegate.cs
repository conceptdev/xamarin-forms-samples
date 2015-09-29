using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Xamarin.Forms;
using RestaurantGuide;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Forms.Platform.iOS;
using CoreSpotlight;


namespace RestaurantGuide.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		UIWindow window;
		List<Restaurant> restaurants;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);

			restaurants = LoadXml ();

			App.SetContent (restaurants);

			LoadApplication (new App ());

			// for debugging Properties
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments); 
			Console.WriteLine (documents);
			// then look in /.config/.isolated-storage/PropertyStore.forms

			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				// Code that requires iOS 9, like CoreSpotlight or 3D Touch
				SearchModel = new iOS9SearchModel (restaurants);
			} else {
				// Code for earlier versions
				Console.WriteLine ("CoreSpotlight not supported");
			}

			return base.FinishedLaunching(app, options);
		}

		List<Restaurant> LoadXml() {
			var restaurants = new List<Restaurant> ();
			#region load data from XML
			using (TextReader reader = new StreamReader("restaurants.xml"))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<Restaurant>));
				restaurants = (List<Restaurant>)serializer.Deserialize(reader);
			}
			#endregion
			return restaurants;
		}

		public iOS9SearchModel SearchModel {
			get;
			private set;
		}

		public override bool ContinueUserActivity (UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
		{
			if (userActivity.ActivityType == CSSearchableItem.ActionType) {
				var uuid = userActivity.UserInfo.ObjectForKey (CSSearchableItem.ActivityIdentifier);

				System.Console.WriteLine ("Show the page for " + uuid);

				var restaurantName = SearchModel.Lookup (uuid.ToString());

				System.Console.WriteLine ("which is " + restaurantName);

				MessagingCenter.Send<RestaurantGuide.App, string> (App.Current as RestaurantGuide.App, "show", restaurantName);

			}
			return true;
		}
	}
}

