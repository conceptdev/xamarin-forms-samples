using System;
using System.Reflection;
using System.Diagnostics;
using System.Resources;
using System.Threading;
using System.Globalization;
using Xamarin.Forms;

namespace Todo
{
	public class L10n
	{
		/// <remarks>
		/// Maybe we can cache this info rather than querying every time
		/// </remarks>
		public static string Locale ()
		{
			return DependencyService.Get<ILocale>().GetCurrent();
		}

		public static string Localize(string key, string comment) {

			var netLanguage = Locale ();

			ResourceManager temp = new ResourceManager("Todo.Resx.AppResources", typeof(L10n).GetTypeInfo().Assembly);

			string result = temp.GetString (key, new CultureInfo (netLanguage));

			return result; 
		}
	}
}

