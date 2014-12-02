using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Todo;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Platform.iOS;

namespace Todo
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.TintColor = UIColor.Black;

			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes { TextColor = UIColor.Black});
			UINavigationBar.Appearance.BarTintColor = UIColor.Black;
			UIBarButtonItem.Appearance.TintColor = UIColor.Black;
			UIBarButtonItem.Appearance.SetTitleTextAttributes (new UITextAttributes { TextColor = UIColor.Black}, UIControlState.Normal );

			var sqliteFilename = "TodoSQLite.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);

			// This is where we copy in the prepopulated database
			Console.WriteLine (path);
			if (!File.Exists (path)) {
				File.Copy (sqliteFilename, path);
			}

			var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			var conn = new SQLite.Net.SQLiteConnection(plat, path);

			var a = new App ();
			// Set the database connection string
			App.SetDatabaseConnection (conn);

			App.SetTextToSpeech (new Speech ());

			LoadApplication (a);

			return base.FinishedLaunching (app, options);
		}
	}
}

