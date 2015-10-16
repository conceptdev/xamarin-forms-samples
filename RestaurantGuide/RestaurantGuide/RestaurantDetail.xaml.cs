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

			Xamarin.Insights.Track("View", new Dictionary<string, string> {
				{"Id", r.Number.ToString()},
				{"Name", r.Name},
//				{"Number", "0123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789a123456789b123456789c123456789d123456789e123456789f123456789"},
//				{"0123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789a123456789b123456789c123456789d123456789e123456789f123456789", "Number"},
//				{"0123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789a123456789b123456789c123456789d123456789e123456789f123456789", "0123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789a123456789b123456789c123456789d123456789e123456789f123456789"},
//				{"Japanese", "レストラン–料理店–飲食店"},
//				{"Korean", "레스토랑–레스토랑–요정"},
//				{"Hebrew", "מסעדה"},
//				{"ChineseT","餐廳–飯店"},
//				{"ChineseS","餐厅–酒家"},
			});

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

