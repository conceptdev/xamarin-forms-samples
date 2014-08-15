using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using MonoTouch.ObjCRuntime;

namespace CalabashDemo.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		static readonly IntPtr setAccessibilityIdentifier_Handle = Selector.GetHandle("setAccessibilityIdentifier:");

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();


			// http://forums.xamarin.com/discussion/21148/calabash-and-xamarin-forms-what-am-i-missing
			Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) => {
				if (null != e.View.StyleId) {
					var intPtr = NSString.CreateNative(e.View.StyleId);
					Messaging.void_objc_msgSend_IntPtr(e.NativeView.Handle, setAccessibilityIdentifier_Handle, intPtr);
					NSString.ReleaseNative(intPtr);
					Console.WriteLine(e.View.StyleId);
				}
			};


			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			window.RootViewController = App.GetMainPage ().CreateViewController ();
			window.MakeKeyAndVisible ();


			#if DEBUG
			Xamarin.Calabash.Start();
			#endif


			return true;
		}
	}
}

