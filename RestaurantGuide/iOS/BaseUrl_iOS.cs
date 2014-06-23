using System;
using MonoTouch.Foundation;
using Xamarin.Forms;
using RestaurantGuide.iOS;

[assembly: Dependency (typeof (BaseUrl_iOS))]

namespace RestaurantGuide.iOS
{
	public class BaseUrl_iOS : IBaseUrl
	{
		#region IBaseUrl implementation

		public string Get ()
		{
			return NSBundle.MainBundle.BundlePath + "/X.png";
		}

		#endregion
	}
}

