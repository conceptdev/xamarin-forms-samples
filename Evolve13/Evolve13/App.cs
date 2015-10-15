using System;
using Xamarin.Forms;

namespace Evolve13
{
	public class App : Application
	{
		public App ()
		{
			var md = new MasterDetailPage ();

			md.Master = new MenuPage (md);
			md.Detail = new NavigationPage(new SessionsPage ()) {Tint = App.NavTint};

			MainPage = md;
		}

		static SQLite.SQLiteConnection conn;
		static EvolveDatabase database;
		public static void SetDatabaseConnection (SQLite.SQLiteConnection connection)
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

