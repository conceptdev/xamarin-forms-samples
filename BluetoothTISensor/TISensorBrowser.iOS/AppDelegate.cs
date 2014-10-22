using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;

namespace TISensorBrowser
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Xamarin.Forms.Forms.Init ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);

			App.SetAdapter (Adapter.Current);

			window.RootViewController = App.GetMainPage ().CreateViewController ();
			window.MakeKeyAndVisible ();

			UINavigationBar.Appearance.TintColor = UIColor.Red;

			return true;
		}
	}
}

