using Xamarin.Forms;

namespace Todo
{
	public class App : Application
	{
		public App ()
		{
			var mainNav = new NavigationPage (new TodoListPage ());
			mainNav.BarBackgroundColor = Color.FromHex ("a97946");
			MainPage = mainNav;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

		static SQLite.Net.SQLiteConnection conn;
		static TodoItemDatabase database;
		public static void SetDatabaseConnection (SQLite.Net.SQLiteConnection connection)
		{
			conn = connection;
			database = new TodoItemDatabase (conn);
		}
		public static TodoItemDatabase Database {
			get { return database; }
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

