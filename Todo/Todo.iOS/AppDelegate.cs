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
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			#if DEBUG
			// http://forums.xamarin.com/discussion/21148/calabash-and-xamarin-forms-what-am-i-missing
			Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) => {

				//Console.WriteLine("=== " + e.View);

				// http://developer.xamarin.com/recipes/testcloud/set-accessibilityidentifier-ios/
				if (null != e.View.StyleId) {
					e.NativeView.AccessibilityIdentifier = e.View.StyleId;
					//Console.WriteLine("Set AccessibilityIdentifier: " + e.View.StyleId);
				}
			};
			#endif

			LoadApplication (new App ());

			return base.FinishedLaunching(app,options);
		}
	}
}

