using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MessageUI;
using Xamarin.Forms;
using EmployeeDirectory.iOS;

[assembly:Dependency(typeof(iOSPhoneFeatureService))]

namespace EmployeeDirectory.iOS
{
	public class iOSPhoneFeatureService : IPhoneFeatureService
	{
		public bool Call (string phoneNumber)
		{
			var url = NSUrl.FromString("tel:" + Uri.EscapeDataString(phoneNumber));

			if (UIApplication.SharedApplication.CanOpenUrl (url)) {
				UIApplication.SharedApplication.OpenUrl (url);
				return true;
			} else {
				return false;
			}
		}

		// http://stackoverflow.com/questions/24136464/access-viewcontroller-in-dependecyservice-to-present-mfmailcomposeviewcontroller/24159484#24159484
		public bool Email (string emailAddress)
		{
			if (MFMailComposeViewController.CanSendMail) {
				var composer = new MFMailComposeViewController ();
				composer.SetToRecipients(new string[] { emailAddress });

				composer.Finished += (object sender, MFComposeResultEventArgs e) => { 
//					if (completed != null)
//						completed (e.Result == MFMailComposeResult.Sent);
					e.Controller.DismissViewController (true, null);
				};

				//Adapt this to your app structure
				var rootController = ((AppDelegate)(UIApplication.SharedApplication.Delegate)).Window.RootViewController.ChildViewControllers[0].ChildViewControllers[1].ChildViewControllers[0];
				var navcontroller = rootController as UINavigationController;
				if (navcontroller != null)
					rootController = navcontroller.VisibleViewController;
				rootController.PresentViewController (composer, true, null);
				return true;
			} else {
				return false;
			}
		}

		public bool Tweet (string twitterName)
		{
			var name = twitterName;
			if (name.StartsWith ("@")) {
				name = name.Substring (1);
			}
			//TODO: really should use TW or Social framework
			var scheme = "twitter://user?screen_name=" + name;
			var url = NSUrl.FromString (scheme);
			if (UIApplication.SharedApplication.CanOpenUrl (url)) {
				UIApplication.SharedApplication.OpenUrl (url);
			} else {
				url = NSUrl.FromString ("http://twitter.com/" + Uri.EscapeDataString (name));
				UIApplication.SharedApplication.OpenUrl (url);
			}

			return true;
		}

		public bool Browse (string websiteUrl)
		{
			UIApplication.SharedApplication.OpenUrl (NSUrl.FromString (websiteUrl));

			return true;
		}

		public bool Map (string address)
		{
			UIApplication.SharedApplication.OpenUrl (
				NSUrl.FromString ("http://maps.google.com/maps?q=" + Uri.EscapeDataString (address)));

			return true;
		}
	}
}

