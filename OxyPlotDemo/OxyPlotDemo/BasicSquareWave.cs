using System;
using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace OxyPlotDemo
{
	/// <summary>
	/// chart data from OxyPlot WPF example
	/// https://oxyplot.codeplex.com/wikipage?title=WpfExample1
	/// </summary>
	public class BasicSquareWave : ContentPage
	{
		public BasicSquareWave ()
		{	this.Title = "Gráfico";
			this.Icon = "Icon.png";

			PlotModel myModel = new PlotModel("Square wave");
			var ls = new LineSeries("sin(x)+sin(3x)/3+sin(5x)/5+...");
			int n = 10;
			for (double x = -10; x < 10; x += 0.0001)
			{
				double y = 0;
				for (int i = 0; i < n; i++)
				{
					int j = i * 2 + 1;
					y += Math.Sin(j * x) / j;
				}
				ls.Points.Add(new DataPoint(x, y));
			}
			myModel.Series.Add(ls);
			myModel.Axes.Add(new LinearAxis(AxisPosition.Left, -4, 4));
			myModel.Axes.Add(new LinearAxis(AxisPosition.Bottom));

			var opv = new OxyPlotView {WidthRequest = 300, HeightRequest = 300};
			opv.Model = myModel;

			this.Content = new StackLayout
			{
				Spacing = 20,
				Padding = 10,
				VerticalOptions = LayoutOptions.Center,
				Children =
				{
					opv
				}
			};
		}
	}
}

