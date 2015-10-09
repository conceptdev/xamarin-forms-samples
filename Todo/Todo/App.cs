using System;
using Xamarin.Forms;

namespace Todo
{
	public class App : Application
	{
		public App ()
		{
			MainPage = new NavigationPage (new TodoListPage ());
		}

		static TodoItemDatabase database;
		public static TodoItemDatabase Database {
			get { 
				if (database == null) {
					database = new TodoItemDatabase ();
				}
				return database; 
			}
		}
	}
}

