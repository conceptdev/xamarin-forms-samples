using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace HttpClientDemo.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();
			Xamarin.FormsMaps.Init();

			LoadApplication(new HttpClientDemo.App());
			
			return base.FinishedLaunching(app,options);
		}
	}
}

