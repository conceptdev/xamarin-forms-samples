using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace PlatformSpecific
{
	/// <summary>
	/// Android app starts with Xamarin.Forms, then opens a natively rendered Page
	/// </summary>
	[Activity (Label = "PlatformSpecific", MainLauncher = true)]
	public class Activity1 : Xamarin.Forms.Platform.Android.AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			// Starts with the main Xamarin.Forms screen
			SetPage (App.GetMainPage ());
		}
	}
}


