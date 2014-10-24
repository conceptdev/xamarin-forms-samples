using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Android.Content.PM;


namespace HeartRateMonitor.Droid
{
	[Activity (Label = "HeartRateXF", 
		MainLauncher = true, 
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			var a = new Robotics.Mobile.Core.Bluetooth.LE.Adapter ();
			App.SetAdapter (a);

			SetPage (App.GetMainPage ());
		}
	}
}

