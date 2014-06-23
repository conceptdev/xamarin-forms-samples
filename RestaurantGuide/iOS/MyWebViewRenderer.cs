using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using RestaurantGuide;
using RestaurantGuide.iOS;
using MonoTouch.Foundation;

[assembly: ExportRenderer (typeof (MyWebView), typeof (MyWebViewRenderer))]

namespace RestaurantGuide.iOS
{
	public class MyWebViewRenderer : WebViewRenderer
	{
		public override void LoadHtmlString (string s, NSUrl baseUrl)
		{
			if (baseUrl == null)
				baseUrl = new NSUrl (NSBundle.MainBundle.BundlePath, true);
			base.LoadHtmlString (s, baseUrl);
		}
	}
}

