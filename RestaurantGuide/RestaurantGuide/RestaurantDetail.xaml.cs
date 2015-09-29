using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RestaurantGuide
{	
	public partial class RestaurantDetail : ContentPage
	{	
		public RestaurantDetail ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			var r = (Restaurant)BindingContext;

			Title = r.Name;

			var template = new RestaurantInfo () { Model = r };
			var page = template.GenerateString ();

			var html = new HtmlWebViewSource {Html = page};
			if (Device.OS != TargetPlatform.iOS) {
				// iOS bug means we're using a custom renderer for now, Android and WP need to implement IBaseUrl
				html.BaseUrl = DependencyService.Get<IBaseUrl> ().Get ();
			}
			webView.Source = html;

			if (Device.OS == TargetPlatform.iOS) {
				DependencyService.Get<IUserActivity> ().Start (r);
			}
		}

		protected override void OnDisappearing ()
		{
			if (Device.OS == TargetPlatform.iOS) {
				DependencyService.Get<IUserActivity> ().Stop ();
			}
			base.OnDisappearing ();
		}
	}
}

