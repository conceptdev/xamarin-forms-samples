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

		public RestaurantList (List<Restaurant> r) : this()
		{
			listView.ItemsSource = r;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			Application.Current.Properties ["rid"] = "";
		}

		public async void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {
			if (e.SelectedItem == null)
				return;
			
			var r = (Restaurant)e.SelectedItem;

			Application.Current.Properties ["rid"] = r.Number;
			await App.Current.SavePropertiesAsync ();

			var rPage = new RestaurantDetail();
			rPage.BindingContext = r;
			await Navigation.PushAsync(rPage);

			((ListView)sender).SelectedItem = null;
		}
	}
}

