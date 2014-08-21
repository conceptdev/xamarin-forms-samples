using System;
using Xamarin.Forms;
using OxyPlot;
using System.Collections.Generic;
using OxyPlot.Series;
using Xamarin;
using System.Diagnostics;
using System.Linq;

namespace OxyPlotDemo
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			var tabs = new TabbedPage ();

			tabs.Children.Add (new EarthquakePage { Title = "Earthquakes" });

			tabs.Children.Add (new WeatherObservationPage { Title = "Weather" });

			return tabs;

//			var earthquakeSeries = new LineSeries ();
//
//			var m = new PlotModel ("Earthquakes");
//
//			var magnitudeAxis = new OxyPlot.Axes.LinearAxis ();
//			magnitudeAxis.Minimum = 0;
//			magnitudeAxis.Maximum = 10;
//			m.Axes.Add (magnitudeAxis);
//
//			var dateAxis = new OxyPlot.Axes.DateTimeAxis ();
//			dateAxis.IntervalType = OxyPlot.Axes.DateTimeIntervalType.Days;
//			dateAxis.StringFormat = "MMMM-yy";
//			m.Axes.Add (dateAxis);
//
//			earthquakeSeries.ItemsSource = new List<DataPoint> (); // empty to start
//			m.Series.Add (earthquakeSeries);
//
//			var opv = new OxyPlotView {
//				WidthRequest = 300, HeightRequest = 300,
//				BackgroundColor = Color.Aqua
//			};
//			opv.Model = m;
//
//			Insights.Track ("SHOWGRAPH");
//
//
//			var l = new Label {
//				Text = "Hello, Oxyplot!",
//				VerticalOptions = LayoutOptions.CenterAndExpand,
//				HorizontalOptions = LayoutOptions.CenterAndExpand,
//			};
//			var b = new Button { Text = "Get Earthquake Data" };
//			b.Clicked += async (sender, e) => {
//				var sv = new GeoNamesWebService();
//				var es = await sv.GetEarthquakesAsync();
//				Xamarin.Forms.Device.BeginInvokeOnMainThread( () => {
//					Debug.WriteLine("found " + es.Length + " earthquakes");
//					l.Text = es.Length + " earthquakes";
//
//					var eqlist = new List<Earthquake>(es);
//					eqlist.Sort((x, y) => string.Compare(x.datetime, y.datetime));
//
//					var pts = new List<DataPoint>();
//					foreach (var eq in eqlist) {
//						pts.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Parse(eq.datetime)), eq.magnitude));
//					}
//
//					earthquakeSeries.ItemsSource = pts;
//					earthquakeSeries.XAxis.CoerceActualMaxMin();
//
//					Device.BeginInvokeOnMainThread(() => {
//						opv.InvalidateDisplay ();
//					});
//				});
//			};
//
//
//			return new ContentPage { 
//				Content = new StackLayout {
//					Children = {
//						l,
//						opv,
//						b
//					}
//				}
//			};
		}
	}
}

