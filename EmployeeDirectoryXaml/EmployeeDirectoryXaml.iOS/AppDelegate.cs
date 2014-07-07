using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using System.IO;
using EmployeeDirectory;

namespace EmployeeDirectory.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		// for Mail Composer, see http://stackoverflow.com/questions/24136464/access-viewcontroller-in-dependecyservice-to-present-mfmailcomposeviewcontroller/24159484#24159484
		public UIWindow Window {
			get { return window; }
		}

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			#region Copy static data into working folder
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath =  documentsPath.Replace("Documents", "Library"); // Library folder

			var path = Path.Combine(libraryPath, "XamarinDirectory.csv");
			//Console.WriteLine (path);
			if (!File.Exists (path)) {
				File.Copy ("XamarinDirectory.csv", path);
			}
			path = Path.Combine(libraryPath, "XamarinFavorites.xml");
			Console.WriteLine (path);
			if (!File.Exists (path)) {
				File.Copy ("XamarinFavorites.xml", path);
			}
			#endregion

			// If you have defined a view, add it here:
			window.RootViewController = App.GetMainPage().CreateViewController ();

			// make the window visible
			window.MakeKeyAndVisible ();

			return true;
		}
	}
}

