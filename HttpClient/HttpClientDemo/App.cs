using System;
using Xamarin.Forms;

namespace HttpClientDemo
{
	public class App : Application
	{
		public App ()
		{	
			var tabs = new TabbedPage ();
			var tabE = new EarthquakesPage () {Title="Earthquakes", Icon="sample-401-globe"};
			var tabA = new AirportsPage() {Title="Airports", Icon="38-airplane"};

			tabs.Children.Add(tabE);
			tabs.Children.Add(tabA);

			MainPage = tabs;
		}
	}
}

