using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TodoXaml;
using Xamarin.Forms;
using System.IO;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Threading.Tasks;

namespace TodoXaml
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;


		MobileServiceClient Client;
		IMobileServiceSyncTable<TodoItem> todoTable;
		TodoItemManager todoItemManager;


		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);


			#region Azure stuff
			CurrentPlatform.Init ();
			SQLitePCL.CurrentPlatform.Init();

			Client = new MobileServiceClient (
				Constants.Url, 
				Constants.Key);	

			#region Azure Sync stuff
			// http://azure.microsoft.com/en-us/documentation/articles/mobile-services-xamarin-android-get-started-offline-data/
			// new code to initialize the SQLite store
			InitializeStoreAsync().Wait();
			#endregion

			todoTable = Client.GetSyncTable<TodoItem>();
			todoItemManager = new TodoItemManager(Client, todoTable);

			App.SetTodoItemManager (todoItemManager);
			#endregion region

			#region Text to Speech stuff
			App.SetTextToSpeech (new Speech ());
			#endregion region

			// If you have defined a view, add it here:
			// window.RootViewController  = navigationController;
			window.RootViewController = App.GetMainPage ().CreateViewController ();

			// make the window visible
			window.MakeKeyAndVisible ();

			return true;
		}

		public async Task InitializeStoreAsync()
		{
			string path = "syncstore.db";
			var store = new MobileServiceSQLiteStore (path);
			store.DefineTable<TodoItem> ();
			await Client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
//				await Client.SyncContext.InitializeAsync (store);
		}
	}
}

