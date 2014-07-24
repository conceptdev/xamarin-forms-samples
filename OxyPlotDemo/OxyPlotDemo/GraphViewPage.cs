using System;
using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Series;
using ExampleLibrary;

namespace OxyPlotDemo
{
	public class GraphViewPage : ContentPage
	{
		public GraphViewPage (ExampleInfo exampleInfo)
		{
			var opv = new OxyPlotView {
				WidthRequest = 300,
				HeightRequest = 300,
			};
			opv.Model = exampleInfo.PlotModel;

			var label = new Label {
				Text = "Category: " + exampleInfo.Category,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};

			Content = new StackLayout {
				BackgroundColor = Color.White,
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					opv,
					label
				}
			};
		}
	}
}

