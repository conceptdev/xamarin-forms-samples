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

		List<Restaurant> restaurants;

		public RestaurantList (List<Restaurant> r) : this()
		{
			restaurants = r;

			listView.ItemsSource = r;
		}

		public void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {
			var r = (Restaurant)e.SelectedItem;

			DisplayAlert (r.Name, "clicked", "ok", null);
		}
	}
}

