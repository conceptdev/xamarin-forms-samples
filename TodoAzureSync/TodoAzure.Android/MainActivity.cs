using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace TodoXaml
{
	[Activity (Label = "TodoXaml", MainLauncher = true)]
	public class Activity1 : Xamarin.Forms.Platform.Android.AndroidActivity
	{
		MobileServiceClient Client;
		IMobileServiceSyncTable<TodoItem> todoTable;
		TodoItemManager todoItemManager;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			#region Azure stuff
			CurrentPlatform.Init ();

			Client = new MobileServiceClient (
				Constants.Url, 
				Constants.Key);		


			#region Azure Sync stuff
			// http://azure.microsoft.com/en-us/documentation/articles/mobile-services-xamarin-android-get-started-offline-data/
			// new code to initialize the SQLite store
			string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "test1.db");

			if (!File.Exists(path))
			{
				File.Create(path).Dispose();
			}

			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<TodoItem>();

			Client.SyncContext.InitializeAsync(store).Wait();
			#endregion


			todoTable = Client.GetSyncTable<TodoItem>(); 
			todoItemManager = new TodoItemManager(Client, todoTable);

			App.SetTodoItemManager (todoItemManager);
			#endregion region

			#region Text to Speech stuff
			App.SetTextToSpeech (new Speech ());
			#endregion

			SetPage (App.GetMainPage ());
		}

		void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}
	}
}


