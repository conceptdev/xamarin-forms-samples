using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using LoginPattern.iOS;

namespace LoginPattern.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate, ILoginManager
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			window.RootViewController = App.GetLoginPage (this).CreateViewController ();
			window.MakeKeyAndVisible ();
			
			return true;
		}

		#region ILoginManager implementation

		// https://developer.apple.com/library/ios/documentation/WindowsViews/Conceptual/WindowAndScreenGuide/WindowScreenRolesinApp/WindowScreenRolesinApp.html
		public void ShowMainPage ()
		{
			window.RootViewController = App.GetMainPage ().CreateViewController ();
			window.MakeKeyAndVisible ();
		}

		public void Logout ()
		{
			window.RootViewController = App.GetLoginPage (this).CreateViewController ();
			window.MakeKeyAndVisible ();
		}

		#endregion
	}
}

