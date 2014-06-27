using System;
using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.OS;

/*
Thanks to
https://forums.xamarin.com/discussion/19362/xamarin-forms-splashscreen-in-android#latest
*/
namespace Evolve13
{
	[Activity(Label = "Evolve", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash", 
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class SplashScreen : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			var intent = new Intent(this, typeof(Activity1));
			StartActivity(intent);
			Finish();
		}
	}
}

