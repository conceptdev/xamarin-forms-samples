using System;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpClientDemo
{
	// http://bertt.wordpress.com/2013/03/19/using-geonames-webservices-from-portable-class-library-pcl/

	public class GeoNamesWebService
	{
		public GeoNamesWebService ()
		{
		}

		public async Task<Earthquake[]> GetEarthquakesAsync () {

			var client = new System.Net.Http.HttpClient ();

			client.BaseAddress = new Uri("http://api.geonames.org/");

			var response = await client.GetAsync("earthquakesJSON?north=44.1&south=-9.9&east=-22.4&west=55.2&username=bertt");

			var earthquakesJson = response.Content.ReadAsStringAsync().Result;

			var rootobject = JsonConvert.DeserializeObject<Rootobject>(earthquakesJson);

			return rootobject.earthquakes;

		}
	}
}

