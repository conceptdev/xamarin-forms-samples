using System;
using Todo;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency (typeof (Todo.SQLite))]

namespace Todo
{
	public class SQLite : ISQLite
	{
		public SQLite ()
		{
		}

		#region ISQLite implementation
		public global::SQLite.Net.SQLiteConnection GetConnection ()
		{
			var sqliteFilename = "TodoSQLite.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);

			// This is where we copy in the prepopulated database
			Console.WriteLine (path);
			if (!File.Exists (path)) {
				File.Copy (sqliteFilename, path);
			}

			var plat = new global::SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			var conn = new global::SQLite.Net.SQLiteConnection(plat, path);

			// Return the database connection 
			return conn;
		}
		#endregion
	}
}
