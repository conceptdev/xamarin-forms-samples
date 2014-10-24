using System;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;

namespace HeartRateMonitor
{
	public class App
	{
		static IAdapter Adapter;

		public static Page GetMainPage ()
		{	
			return new NavigationPage (new DeviceList (Adapter));
		}

		public static void SetAdapter (IAdapter adapter) {
			Adapter = adapter;
		}
	}
}

