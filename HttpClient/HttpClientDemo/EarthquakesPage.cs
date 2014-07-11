using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace HttpClientDemo
{

	public class EarthquakesPage : ContentPage
	{
		ListView lv;
		Label l;

		public EarthquakesPage ()
		{
			l = new Label { Text = "Earthquakes", Font = Font.BoldSystemFontOfSize(NamedSize.Large) };

			var b = new Button { Text = "Get Earthquakes" };
			b.Clicked += async (sender, e) => {
				var sv = new GeoNamesWebService();
				var es = await sv.GetEarthquakesAsync();
				Xamarin.Forms.Device.BeginInvokeOnMainThread( () => {
					Debug.WriteLine("found " + es.Length + " earthquakes");
					l.Text = es.Length + " earthquakes";
					lv.ItemsSource = es;
				});
			};

			lv = new ListView ();
			lv.ItemTemplate = new DataTemplate(typeof(TextCell));
			lv.ItemTemplate.SetBinding(TextCell.TextProperty, "Summary");
			lv.ItemSelected += (sender, e) => {
				var eq = (Earthquake)e.SelectedItem;
				DisplayAlert("Earthquake info", eq.ToString(), "OK", null);
			};

			Content = new StackLayout {
				Padding = new Thickness (0, 20, 0, 0),
				Children = {
					l,
					b,
					lv
				}
			};
		}
	}
}

