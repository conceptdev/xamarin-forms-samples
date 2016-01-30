using System;
using Xamarin.Forms;

namespace HttpClientDemo
{
	public class App : Application
	{
		public App ()
		{	
			MainPage = new EarthquakesPage ();
		}
	}
}

