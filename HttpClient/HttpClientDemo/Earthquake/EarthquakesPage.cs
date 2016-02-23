using System;
using Xamarin.Forms;
using System.Diagnostics;
using Xamarin.Forms.Maps;

namespace HttpClientDemo
{
	public class EarthquakesPage : ContentPage
	{
		ListView lv;
		Label l;
		Map m;

		public EarthquakesPage ()
		{
			l = new Label { Text = "Earthquakes", 
				FontAttributes = FontAttributes.Bold, 
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) };

			var b = new Button { Text = "Get Earthquakes" };
			b.Clicked += async (sender, e) => {
				var sv = new GeoNamesWebService();
				var es = await sv.GetEarthquakesAsync();
				Xamarin.Forms.Device.BeginInvokeOnMainThread( () => {
					Debug.WriteLine("found " + es.Length + " earthquakes");
					l.Text = es.Length + " earthquakes";
					lv.ItemsSource = es;

					if (Device.OS == TargetPlatform.iOS)
					{	// only put maps on iOS, so much hassle with keys on other platforms
						foreach (var eq in es)
						{
							var p = new Pin();
							p.Position = new Position(eq.lat, eq.lng);
							p.Label = "Magnitude: " + eq.magnitude;
							m.Pins.Add(p);
						}
					}
				});
			};

			lv = new ListView ();
			lv.ItemTemplate = new DataTemplate(typeof(TextCell));
			lv.ItemTemplate.SetBinding(TextCell.TextProperty, "Summary");
			lv.ItemSelected += async (sender, e) => {
				var eq = (Earthquake)e.SelectedItem;
				await DisplayAlert("Earthquake info", eq.ToString(), "OK");
			};


			if (Device.OS == TargetPlatform.iOS)
			{	// only put maps on iOS, so much hassle with keys on other platforms
				m = new Map();
				m.HeightRequest = 200;

				Content = new StackLayout
				{
					Padding = new Thickness(0, 20, 0, 0),
					Children = {
						l,
						b,
						m,
						lv
					}
				};
			}
			else
			{ 
				Content = new StackLayout
				{
					Padding = new Thickness(0, 20, 0, 0),
					Children = {
						l,
						b,
						lv
					}
				};
			}
		}
	}
}

