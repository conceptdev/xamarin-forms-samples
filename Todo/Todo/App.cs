using System;
using Xamarin.Forms;

namespace Todo
{
	public class App : Application
	{
		public App ()
		{
			var tp = new TabbedPage();

			tp.Children.Add (new NavigationPage (new TodoListPage ()) {Title = "C#" } );
			tp.Children.Add (new NavigationPage (new TodoListXaml ()) {Title = "XAML"} );

			MainPage = tp;
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

