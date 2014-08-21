using System;
using System.Collections.Generic;

namespace OxyPlotDemo
{
	// http://json2csharp.com/
	public class WeatherObservation
	{
		public string clouds { get; set; }
		public string weatherCondition { get; set; }
		public string observation { get; set; }
		public int windDirection { get; set; }
		public string ICAO { get; set; }
		public double lng { get; set; }
		public string temperature { get; set; }
		public string dewPoint { get; set; }
		public string weatherConditionCode { get; set; }
		public string windSpeed { get; set; }
		public int humidity { get; set; }
		public string stationName { get; set; }
		public string datetime { get; set; }
		public double lat { get; set; }
		public string cloudsCode { get; set; }
		public int? hectoPascAltimeter { get; set; }
	}

	public class RootObjectWO
	{
		public WeatherObservation[] weatherObservations { get; set; }
	}
}

