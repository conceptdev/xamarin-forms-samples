using System;
using Xamarin.Forms;

namespace TodoMvvm
{
	public static class App
	{
		static void RegisterTypes ()
		{
			// This can be replaced by any number of MVVM tools. It is done this way merely because this 
			// is not intended to be a demo of those tools.
			ViewFactory.Register<TodoListPage, TodoListViewModel> ();
			ViewFactory.Register<TodoItemPage, TodoItemViewModel> ();
		}

		public static Page GetMainPage ()
		{
			RegisterTypes ();
			var mainNav = new NavigationPage (ViewFactory.CreatePage<TodoListViewModel> ());

			MessagingCenter.Subscribe<TodoListViewModel, TodoItem> (mainNav, "TodoItemSelected", (sender, viewModel) => {
				var todovm = new TodoItemViewModel (viewModel);
				mainNav.Navigation.PushAsync (ViewFactory.CreatePage (todovm));
			});

			return mainNav;
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

