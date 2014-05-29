using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TodoXaml;
using Xamarin.Forms;
using TodoXaml.iOS;

namespace TodoXaml
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		TodoItemManager todoItemManager;


		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);


			#region Parse stuff
			todoItemManager = new TodoItemManager(ParseStorage.Default);

			App.SetTodoItemManager (todoItemManager);

			#endregion

			#region Text to Speech stuff
			App.SetTextToSpeech (new Speech ());
			#endregion region

			// If you have defined a view, add it here:
			// window.RootViewController  = navigationController;
			window.RootViewController = App.GetMainPage ().CreateViewController ();

			// make the window visible
			window.MakeKeyAndVisible ();

			return true;
		}
	}
}

