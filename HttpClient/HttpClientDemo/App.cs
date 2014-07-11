using System;
using Xamarin.Forms;

namespace HttpClientDemo
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			return new EarthquakesPage ();
		}
	}
}

