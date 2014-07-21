using System;
using Xamarin.Forms;
using MonoTouch.Foundation;

[assembly:Dependency(typeof(Todo.iOS.Locale_iOS))]

namespace Todo.iOS
{
	public class Locale_iOS : Todo.ILocale
	{
		/// <remarks>
		/// Not sure if we can cache this info rather than querying every time
		/// </remarks>
		public string GetCurrent ()
		{
			#region not sure why this isn't working for me (in simulator at least)
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

			// HACK: not sure why NSLocale isn't ever returning correct data
			if (NSLocale.PreferredLanguages.Length > 0) {
				var pref = NSLocale.PreferredLanguages [0];
				netLanguage = pref.Replace ("_", "-");
				Console.WriteLine ("preferred:" + netLanguage);
			}
			return netLanguage;
		}
	}
}

