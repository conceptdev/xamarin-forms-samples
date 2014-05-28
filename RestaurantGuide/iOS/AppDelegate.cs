using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using RestaurantGuide;
using System.IO;
using System.Xml.Serialization;


namespace RestaurantGuide.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);

			App.SetContent (LoadXml());

			window.RootViewController = App.GetMainPage ().CreateViewController ();
			window.MakeKeyAndVisible ();
			
			return true;
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
	}
}

