using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Roget1911
{
	public static class App
	{
		public static XmlLoader XmlData;

		public static Page GetMainPage ()
		{
			var mainNav = new NavigationPage (new MainListPage ());


			//
			// This class uses PCLstorage, so we can write cross-platform
			// loading of XML files, as long as they are in the expected place
			// (see AppDelegate and MainActivity for where that is :)
			//
			XmlData = new XmlLoader();

			return mainNav;
		}

		public static async Task LoadXml() {
			await XmlData.LoadXml ();
		}

		static ITextToSpeech TextToSpeech;
		public static void SetTextToSpeech (ITextToSpeech speech)
		{
			TextToSpeech = speech;
		}
		public static ITextToSpeech Speech {
			get { return TextToSpeech; }
		}
	}
}

