using System;
using Xamarin.Forms;
using System.Threading;

[assembly:Dependency(typeof(Todo.Android.Locale_Android))]

namespace Todo.Android
{
	public class Locale_Android : Todo.ILocale
	{
		/// <remarks>
		/// Not sure if we can cache this info rather than querying every time
		/// </remarks>
		public string GetCurrent() 
		{
			var androidLocale = Java.Util.Locale.Default;

			//var netLanguage = androidLocale.Language.Replace ("_", "-");
			var netLanguage = androidLocale.ToString().Replace ("_", "-");

			//var netLanguage = androidLanguage.Replace ("_", "-");
			Console.WriteLine ("ios:" + androidLocale.ToString());
			Console.WriteLine ("net:" + netLanguage);

			Console.WriteLine (Thread.CurrentThread.CurrentCulture);
			Console.WriteLine (Thread.CurrentThread.CurrentUICulture);

			return netLanguage;
		}
	}
}

