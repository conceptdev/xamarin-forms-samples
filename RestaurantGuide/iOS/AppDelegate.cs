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


namespace RestaurantGuide.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);

			App.SetContent (LoadXml());

			LoadApplication (new App ());

			// for debugging Properties
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments); 
			Console.WriteLine (documents);
			// then look in /.config/.isolated-storage/PropertyStore.forms

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
	}
}

