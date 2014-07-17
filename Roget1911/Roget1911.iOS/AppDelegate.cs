using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using System.IO;

namespace Roget1911
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			#region copy xml into a place where PCLStorage can find it!
			var filename = "roget15aCategories.xml";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, filename);

			// This is where we copy in the prepopulated database
			Console.WriteLine (path);
			if (!File.Exists (path)) {
				File.Copy (filename, path);
			}
			filename = "roget15aHierarchy.xml";
			documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			path = Path.Combine(libraryPath, filename);

			// This is where we copy in the prepopulated database
			Console.WriteLine (path);
			if (!File.Exists (path)) {
				File.Copy (filename, path);
			}
			#endregion

			App.SetTextToSpeech (new Speech ());

			// If you have defined a view, add it here:
			// window.RootViewController  = navigationController;
			window.RootViewController = App.GetMainPage ().CreateViewController ();

			// make the window visible
			window.MakeKeyAndVisible ();

			return true;
		}
	}
}

