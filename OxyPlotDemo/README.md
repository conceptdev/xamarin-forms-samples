# [OxyPlot](http://oxyplot.org) Demo (Xamarin.Forms)

Uses source from [oxyplot.codeplex.com](https://oxyplot.codeplex.com/SourceControl/latest) to build:

* PCL core
* Xamarin.iOS assembly
* Xamarin.Android assembly

The core can be used in Xamarin.Forms and the platform-specific assemblies used to build [Custom Renderers](http://developer.xamarin.com/guides/cross-platform/xamarin-forms/custom-renderer/). The entire [OxyPlot test suite](http://oxyplot.codeplex.com/SourceControl/latest#README) is a PCL that can be referenced in Xamarin.Forms:

[Game of Life animation (YouTube)](http://youtu.be/IbKNCpdV1bE)

![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/OxyPlotDemo/Screenshots/gameoflife-sml.png "Game of Life")

![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/OxyPlotDemo/Screenshots/linearcoloraxis-sml.png "Linear Color")

![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/OxyPlotDemo/Screenshots/pieseries-sml.png "Pie")


##Simple demo

This sample code to get started came from the [WPF demo](http://oxyplot.org/doc/HelloWpfXaml.html).


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
			return new ContentPage { 
				Content = new StackLayout {
					Children = {
						label1, 
						opv,
						label2
					}
				}
			};


![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/OxyPlotDemo/Screenshots/iOS-sml.png "Android") ![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/OxyPlotDemo/Screenshots/Android-sml.png "Android") ![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/OxyPlotDemo/Screenshots/WinPhone-sml.png "Android")

##WARNING

This isn't finished yet - currently just playing around...