using System;
using Xamarin.Forms;
using OxyPlot;
using System.Collections.Generic;
using OxyPlot.Series;

namespace OxyPlotDemo
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			return new NavigationPage(new ExampleList {Title = "OxyPlot Examples" });

			// 
			// http://forums.xamarin.com/discussion/21031/oxyplot-chart-inside-a-contentpage#latest
			//
//			return new BasicSquareWave ();

			//
			// this was the original test code
			//
//			return new BasicTest ();

		}
	}
}

