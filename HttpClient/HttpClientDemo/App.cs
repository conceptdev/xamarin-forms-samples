using System;
using Xamarin.Forms;

namespace HttpClientDemo
{
	public class App : Application
	{
		public App ()
		{	
			var tabs = new TabbedPage ();
			var tabE = new EarthquakesPage () {Title="Earthquakes"};
			var tabA = new AirportsPage() {Title="Airports"};

			tabs.Children.Add(tabE);
			tabs.Children.Add(tabA);

			MainPage = tabs;
		}
	}
}

