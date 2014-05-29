using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Microsoft.WindowsAzure.MobileServices;

namespace TodoXaml
{
	[Activity (Label = "TodoXaml", MainLauncher = true)]
	public class Activity1 : Xamarin.Forms.Platform.Android.AndroidActivity
	{
		MobileServiceClient Client;
		IMobileServiceTable<TodoItem> todoTable;
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
			todoTable = Client.GetTable<TodoItem>(); 
			todoItemManager = new TodoItemManager(todoTable);

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


