using System;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;

namespace TISensorBrowser
{
	public class App
	{
		static IAdapter Adapter;

		public static Page GetMainPage ()
		{	
			var np = new NavigationPage (new DeviceList (Adapter));
			if (Device.OS != TargetPlatform.iOS) {
				// we manage iOS themeing via the native app Appearance API
				np.BarBackgroundColor = Color.Red;
			}
			return np;
		}

		public static void SetAdapter (IAdapter adapter) {
			Adapter = adapter;
		}
	}
}

