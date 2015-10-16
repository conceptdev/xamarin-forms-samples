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
		public static AppDelegate Current {get;set;}
		public UIWindow window;
		List<Restaurant> restaurants;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Current = this;
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
				SearchModel = new SpotlightHelper (restaurants);
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


		public override bool ContinueUserActivity (UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
		{
			if (userActivity.ActivityType == CSSearchableItem.ActionType) {
				#region Spotlight
				var uuid = userActivity.UserInfo.ObjectForKey (CSSearchableItem.ActivityIdentifier);
				Xamarin.Insights.Track("SearchResult", new Dictionary<string, string> {
					{"Type", "CoreSpotlight"}
				});
				System.Console.WriteLine ("Show the page for " + uuid);

				var restaurantName = SearchModel.Lookup (uuid.ToString ());

				System.Console.WriteLine ("which is " + restaurantName);

				MessagingCenter.Send<RestaurantGuide.App, string> (App.Current as RestaurantGuide.App, "show", restaurantName);
				#endregion
			} else {
				#region NSUserActivity
				// dang it, the userInfo is blank unless I hack the UserActivity_iOS.Start() method
				// https://forums.developer.apple.com/thread/9690
				if (userActivity.ActivityType == ActivityTypes.View)
				{
					Xamarin.Insights.Track("SearchResult", new Dictionary<string, string> {
						{"Type", "NSUserActivity"}
					});
					var uid = "0";
					if (userActivity.UserInfo.Count == 0) {
						// new item

					} else {
						uid = userActivity.UserInfo.ObjectForKey (ActivityKeys.Id).ToString ();
						if (uid == "0") {
							Console.WriteLine ("No userinfo found for " + ActivityTypes.View);
						} else {
							Console.WriteLine ("Should display id " + uid);
							// handled in DetailViewController.RestoreUserActivityState
						}
					}
					ContinueNavigation (uid);
				}
				if (userActivity.ActivityType == CSSearchableItem.ActionType) {
					Xamarin.Insights.Track("SearchResult", new Dictionary<string, string> {
						{"Type", "CoreSpotlight"}
					});
					var uid = userActivity.UserInfo.ObjectForKey (CSSearchableItem.ActivityIdentifier).ToString();

					System.Console.WriteLine ("Show the detail for id:" + uid);

					ContinueNavigation (uid);
				}
				completionHandler(null); // TODO: display UI in Forms somehow
				#endregion
			}
			return true;
		}

		#region Spotlight
		public SpotlightHelper SearchModel {
			get;
			private set;
		}
		#endregion


		#region Quick Action
		public UIApplicationShortcutItem LaunchedShortcutItem { get; set; }
		public override void OnActivated (UIApplication application)
		{
			Console.WriteLine ("ccccccc OnActivated");

			// Handle any shortcut item being selected
			HandleShortcutItem(LaunchedShortcutItem);

			Xamarin.Insights.Track("3DTouch", new Dictionary<string, string> {
				{"Type", "Random"}
			});

			// Clear shortcut after it's been handled
			LaunchedShortcutItem = null;
		}
		// if app is already running
		public override void PerformActionForShortcutItem (UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
		{
			Console.WriteLine ("dddddddd PerformActionForShortcutItem");
			// Perform action
			var handled = HandleShortcutItem(shortcutItem);
			completionHandler(handled);
		}
		public bool HandleShortcutItem(UIApplicationShortcutItem shortcutItem) {
			Console.WriteLine ("eeeeeeeeeee HandleShortcutItem ");
			var handled = false;

			// Anything to process?
			if (shortcutItem == null) return false;

			// Take action based on the shortcut type
			switch (shortcutItem.Type) {
			case RestaurantGuide.iOS.ShortcutHelper.ShortcutIdentifiers.Random:
				Console.WriteLine ("QUICKACTION: Choose Random Restaurant");

				//HACK: show the detail with a random restaurant showing
				ContinueNavigation ("-1");

				handled = true;
				break;
			}

			Console.Write (handled);
			// Return results
			return handled;
		}
		#endregion




		void ContinueNavigation (string uid){
			Console.WriteLine ("gggggggggg ContinueNavigation");

			// TODO: display UI in Forms somehow
			System.Console.WriteLine ("Show the page for " + uid);

			var restaurantName = "";
			if (uid == "-1")
				restaurantName = SearchModel.Random ();
			else
				restaurantName = SearchModel.Lookup (uid.ToString());

			System.Console.WriteLine ("which is " + restaurantName);

			MessagingCenter.Send<RestaurantGuide.App, string> (App.Current as RestaurantGuide.App, "show", restaurantName);
		}
	}
}

