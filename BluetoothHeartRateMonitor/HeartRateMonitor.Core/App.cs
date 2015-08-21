using System;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;

namespace HeartRateMonitor
{
	public class App : Application
	{
		static IAdapter Adapter;

		public App ()
		{	
			MainPage = new NavigationPage (new DeviceList (Adapter));
		}

		public static void SetAdapter (IAdapter adapter) {
			Adapter = adapter;
		}
	}
}

