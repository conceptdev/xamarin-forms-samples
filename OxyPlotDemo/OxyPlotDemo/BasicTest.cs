using System;
using Xamarin.Forms;
using OxyPlot;
using System.Collections.Generic;
using OxyPlot.Series;

namespace OxyPlotDemo
{
	public class BasicTest : ContentPage
	{
		public BasicTest ()
		{
			// uses the sample basics from http://oxyplot.org/doc/HelloWpfXaml.html
			var Points = new List<DataPoint>
			{
				new DataPoint(0, 4),
				new DataPoint(10, 13),
				new DataPoint(20, 15),
				new DataPoint(30, 16),
				new DataPoint(40, 12),
				new DataPoint(50, 12)
			};

			var m = new PlotModel ("Titleee");
			m.PlotType = PlotType.XY;

			var s = new LineSeries ();
			s.ItemsSource = Points;
			m.Series.Add (s);

			var opv = new OxyPlotView {
				WidthRequest = 300, HeightRequest = 300,
				BackgroundColor = Color.Aqua
			};
			opv.Model = m;

			Content = new StackLayout {
				Children = {
						new Label {
						Text = "Hello, Oxyplot!",
						VerticalOptions = LayoutOptions.CenterAndExpand,
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						},
					opv,
					new Label {
						Text = "http://oxyplot.org/doc/HelloWpfXaml.html",
						Font = Font.SystemFontOfSize(NamedSize.Small),
						VerticalOptions = LayoutOptions.CenterAndExpand,
						HorizontalOptions = LayoutOptions.CenterAndExpand,
					},
				}
			};
		}
	}
}

