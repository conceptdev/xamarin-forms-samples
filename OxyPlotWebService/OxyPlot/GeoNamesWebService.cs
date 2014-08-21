using System;
using System.Threading.Tasks;
using OxyPlotDemo;
using Newtonsoft.Json;

namespace OxyPlotDemo
{
	// http://bertt.wordpress.com/2013/03/19/using-geonames-webservices-from-portable-class-library-pcl/

	public class GeoNamesWebService
	{
		public async Task<Earthquake[]> GetEarthquakesAsync () {

			var client = new System.Net.Http.HttpClient ();

			client.BaseAddress = new Uri("http://api.geonames.org/");

			var response = await client.GetAsync("earthquakesJSON?north=44.1&south=-9.9&east=-22.4&west=55.2&username=bertt&maxRows=20");

			var earthquakesJson = response.Content.ReadAsStringAsync().Result;

			var rootobject = JsonConvert.DeserializeObject<RootobjectE>(earthquakesJson);

			return rootobject.earthquakes;

		}

		public async Task<WeatherObservation[]> GetWeatherObservationsAsync () {

			var client = new System.Net.Http.HttpClient ();

			client.BaseAddress = new Uri("http://api.geonames.org/");

			//findNearByWeatherJSON?lat=43&lng=-2&username=demo
			var response = await client.GetAsync("weatherJSON?north=44.1&south=-9.9&east=-22.4&west=55.2&username=bertt&maxRows=20");

			var earthquakesJson = response.Content.ReadAsStringAsync().Result;

			var rootobject = JsonConvert.DeserializeObject<RootObjectWO>(earthquakesJson);

			return rootobject.weatherObservations;

		}
	}
}

