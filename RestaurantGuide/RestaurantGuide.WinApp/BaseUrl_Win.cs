using System;
using Xamarin.Forms;
using RestaurantGuide.iOS;

[assembly: Dependency (typeof (BaseUrl_Win))]

namespace RestaurantGuide.iOS
{
	public class BaseUrl_Win : IBaseUrl
	{
		#region IBaseUrl implementation

		public string Get ()
		{
            return "";
		}

		#endregion
	}
}

