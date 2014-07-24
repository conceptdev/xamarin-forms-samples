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
			// this was the original test code
			//
//			return new BasicTest ();

		}
	}
}

