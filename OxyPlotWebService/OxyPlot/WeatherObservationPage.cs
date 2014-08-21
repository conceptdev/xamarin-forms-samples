using System;
using Xamarin.Forms;
using OxyPlot.Series;
using OxyPlot;
using Xamarin;
using System.Diagnostics;
using System.Collections.Generic;

namespace OxyPlotDemo
{
	public class WeatherObservationPage : ContentPage
	{
		public WeatherObservationPage ()
		{
			var weatherSeries = new LineSeries ();

			var m = new PlotModel ("Weather");

			var magnitudeAxis = new OxyPlot.Axes.LinearAxis ();
			magnitudeAxis.Minimum = 0;
			magnitudeAxis.Maximum = 40;
			magnitudeAxis.Title = "Temperature";
			m.Axes.Add (magnitudeAxis);

			var dateAxis = new OxyPlot.Axes.CategoryAxis ();
			dateAxis.Labels.Add("Station");
			m.Axes.Add (dateAxis);

			weatherSeries.ItemsSource = new List<DataPoint> (); // empty to start
			m.Series.Add (weatherSeries);

			var opv = new OxyPlotView {
				WidthRequest = 300, HeightRequest = 300,
				BackgroundColor = Color.Aqua
			};
			opv.Model = m;

			Insights.Track ("SHOWGRAPH");


			var l = new Label {
				Text = "Hello, Oxyplot!",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};
			var b = new Button { Text = "Get Weather Data" };
			b.Clicked += async (sender, e) => {
				var sv = new GeoNamesWebService();
				var we = await sv.GetWeatherObservationsAsync();
				Xamarin.Forms.Device.BeginInvokeOnMainThread( () => {
					Debug.WriteLine("found " + we.Length + " weather observations");
					l.Text = we.Length + " weather observations";

					var eqlist = new List<WeatherObservation>(we);
//					eqlist.Sort((x, y) => string.Compare(x.datetime, y.datetime));


					var columSeries = new ColumnSeries();


					foreach (var eq in eqlist) {
						double t = 0.0;
						Double.TryParse(eq.temperature, out t);
						columSeries.Items.Add(new ColumnItem(t, 0));
						//pts.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Parse(eq.datetime)), Double.Parse(eq.temperature)));
					}
					opv.Model.Series.Clear();
					opv.Model.Series.Add(columSeries);

					Device.BeginInvokeOnMainThread(() => {
						opv.InvalidateDisplay ();
					});
				});
			};


			Padding = new Thickness (0, 20, 0, 0);
			Content = new StackLayout {
				Children = {
					opv,
					b,
					l
				}
			};
		}
	}
}

