using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HttpClientDemo
{
	public partial class AirportsPage : ContentPage
	{
		public AirportsPage()
		{
			InitializeComponent();

			airportButton.Clicked += async (s, e) => {
				var code = airportCode.Text;
				airportInfo.Text = await GetEarthquakesAsync(code);
			};
		}


		// http://services.faa.gov/docs/faq/

		public async Task<string> GetEarthquakesAsync (string code) {

			var client = new System.Net.Http.HttpClient ();

			var address = $"http://services.faa.gov/airport/status/{code}?format=application/json";

			client.BaseAddress = new Uri(address);

			var response = await client.GetAsync("");

			var airportJson = response.Content.ReadAsStringAsync().Result;

			//var rootobject = JsonConvert.DeserializeObject<Rootobject>(airportJson);

			return airportJson;

		}
	}
}

