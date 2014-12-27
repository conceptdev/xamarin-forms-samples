using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RestaurantGuide
{	
	public partial class RestaurantList : ContentPage
	{	
		public RestaurantList ()
		{
			InitializeComponent ();
		}

		//List<Restaurant> restaurants;

		public RestaurantList (List<Restaurant> r) : this()
		{
			//restaurants = r;

			listView.ItemsSource = r;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			Application.Current.Properties ["rid"] = "";
		}

		public void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {
			var r = (Restaurant)e.SelectedItem;

			Application.Current.Properties ["rid"] = r.Number;

			var rPage = new RestaurantDetail();
			rPage.BindingContext = r;
			Navigation.PushAsync(rPage);
		}
	}
}

