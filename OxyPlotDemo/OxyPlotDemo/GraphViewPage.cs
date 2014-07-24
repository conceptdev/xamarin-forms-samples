using System;
using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Series;
using ExampleLibrary;

namespace OxyPlotDemo
{
	public class GraphViewPage : ContentPage
	{
		bool keepRunning = false;

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

			bool GameOfLife = exampleInfo.Title.Contains ("Conway");
			if (!GameOfLife) {
				Content = new StackLayout {
					BackgroundColor = Color.White,
					Orientation = StackOrientation.Vertical,
					VerticalOptions = LayoutOptions.StartAndExpand,
					Children = {
						opv,
						label
					}
				};
			} else {

				var stepButton = new Button {
					Text = "step"
				};
				stepButton.Clicked += (sender, e) => {
					var ea = new OxyMouseDownEventArgs ();
					ea.ChangedButton = OxyMouseButton.Left;
					ea.Position = new ScreenPoint (150, 150);
					opv.Model.HandleMouseDown (this, ea);
					opv.InvalidateDisplay ();
				};


				var startButton = new Button {
					Text = "run"
				};
				startButton.Clicked += (sender, e) => {
					if (!keepRunning) {
						keepRunning = true;
						Device.StartTimer (new TimeSpan (0, 0, 0, 0, 200), () => {
							var ea = new OxyMouseDownEventArgs ();
							ea.ChangedButton = OxyMouseButton.Left;
							ea.Position = new ScreenPoint (150, 150);
							opv.Model.HandleMouseDown (this, ea);
							Device.BeginInvokeOnMainThread(() => {
								opv.InvalidateDisplay ();
							});
							return keepRunning;
						});
					}
				};
				var stopButton = new Button {
					Text = "stop"
				};
				stopButton.Clicked += (sender, e) => {
					keepRunning = false;
				};

				Content = new StackLayout {
					BackgroundColor = Color.White,
					Orientation = StackOrientation.Vertical,
					VerticalOptions = LayoutOptions.StartAndExpand,
					Children = {
						opv,
						label,
						new StackLayout {
							HorizontalOptions = LayoutOptions.CenterAndExpand,
							Orientation = StackOrientation.Horizontal,
							Children = { stepButton, startButton, stopButton }
						}

					}
				};
			}
		}
	}
}

