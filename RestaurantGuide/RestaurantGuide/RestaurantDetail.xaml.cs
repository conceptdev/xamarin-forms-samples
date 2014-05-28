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
			webView.Source = new HtmlWebViewSource {Html = page};
		}
	}
}

