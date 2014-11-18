using System;
using Xamarin.Forms;

namespace Todo
{
	public static class App
	{
		public static Page GetMainPage ()
		{
			var mainNav = new NavigationPage (new TodoListPage ());
			return mainNav;
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

