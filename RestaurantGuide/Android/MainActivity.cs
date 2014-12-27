using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

using Xamarin.Forms.Platform.Android;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;


namespace RestaurantGuide.Android
{
	[Activity (Label = "RestaurantGuide", 
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			App.SetContent (LoadXml ());

			LoadApplication (new App ());
		}


		List<Restaurant> LoadXml() {
			var restaurants = new List<Restaurant> ();
			#region load data from XML
			var s = Resources.OpenRawResource(Resource.Raw.restaurants);
			using (TextReader reader = new StreamReader(s))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<Restaurant>));
				restaurants = (List<Restaurant>)serializer.Deserialize(reader);
			}
			#endregion
			return restaurants;
		}


		// readStream is the stream you need to read
		// writeStream is the stream you want to write to
		void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}
	}
}

