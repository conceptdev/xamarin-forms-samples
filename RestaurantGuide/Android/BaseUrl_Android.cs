using System;
using Xamarin.Forms;
using RestaurantGuide.Android;

[assembly: Dependency (typeof (BaseUrl_Android))]

namespace RestaurantGuide.Android
{
	public class BaseUrl_Android : IBaseUrl
	{
		#region IBaseUrl implementation

		public string Get ()
		{
			return Forms.Context.ApplicationInfo.DataDir;
		}

		#endregion
	}
}

