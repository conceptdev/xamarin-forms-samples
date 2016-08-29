using System;
using Xamarin.Forms;
using MonoTouch.Foundation;

[assembly:Dependency(typeof(Todo.iOS.Locale_iOS))]

namespace Todo.iOS
{
	/// <remarks>
	/// iOS supported languages
	/// http://www.ibabbleon.com/iOS-Language-Codes-ISO-639.html
	/// 
	/// .NET supported cultures
	/// http://www.localeplanet.com/dotnet/
	/// </remarks>
	public class Locale_iOS : Todo.ILocale
	{
		/// <remarks>
		/// Not sure if we can cache this info rather than querying every time
		/// </remarks>
		public string GetCurrent ()
		{
			#region output all the values for testing - not needed in production code
			var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
			var iosLanguageAuto = NSLocale.AutoUpdatingCurrentLocale.LanguageCode;
			Console.WriteLine ("nslocaleid:" + iosLocaleAuto);
			Console.WriteLine ("nslanguage:" + iosLanguageAuto);

			var iosLocale = NSLocale.CurrentLocale.LocaleIdentifier;
			var iosLanguage = NSLocale.CurrentLocale.LanguageCode;

			var netLocale = iosLocale.Replace ("_", "-");
			var netLanguage = iosLanguage.Replace ("_", "-");

			Console.WriteLine ("ios:" + iosLanguage + " " + iosLocale);
			Console.WriteLine ("net:" + netLanguage + " " +  netLocale);

			// doesn't seem to affect anything (well, i didn't expect it to affect UIKit controls)
//			var ci = new System.Globalization.CultureInfo ("JA-jp");
//			System.Threading.Thread.CurrentThread.CurrentCulture = ci;

			#endregion

			if (NSLocale.PreferredLanguages.Length > 0) 
			{
				var pref = NSLocale.PreferredLanguages [0];
				Console.WriteLine("NSLocale.PreferredLanguages [0]:" + NSLocale.PreferredLanguages[0]);

				netLanguage = pref.Replace ("_", "-"); // for .NET-ification

				// -- Handling unsupported langauge codes --
				// Schwiizertüütsch (Swiss German)
				if (NSLocale.AutoUpdatingCurrentLocale.LanguageCode == "gsw")
				{
					netLanguage = "de-CH"; // TODO: attempt to detect/use locale set by user too
				}

			}

			Console.WriteLine("preferred now:" + netLanguage); 
			return netLanguage;
		}
	}
}

