using System;
using Xamarin.Forms;

namespace Evolve13
{
	public static class App
	{
		public static Page GetMainPage ()
		{
			var md = new MasterDetailPage ();

			md.Master = new MenuPage (md);
			md.Detail = new NavigationPage(new SessionsPage ()) {Tint = App.NavTint};

			return md;
		}

		static SQLite.Net.SQLiteConnection conn;
		static EvolveDatabase database;
		public static void SetDatabaseConnection (SQLite.Net.SQLiteConnection connection)
		{
			conn = connection;
			database = new EvolveDatabase (conn);
		}
		public static EvolveDatabase Database {
			get { return database; }
		}


		public static Color NavTint {
			get {
				return Color.FromHex ("3498DB"); // Xamarin Blue
			}
		}
		public static Color HeaderTint {
			get {
				return Color.FromHex ("2C3E50"); // Xamarin DarkBlue
			}
		}
	}
}

