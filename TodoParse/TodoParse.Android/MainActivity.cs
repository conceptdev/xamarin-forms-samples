using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using TodoXaml.Android;

namespace TodoXaml
{
	[Activity (Label = "TodoXaml", MainLauncher = true)]
	public class Activity1 : Xamarin.Forms.Platform.Android.AndroidActivity
	{
		TodoItemManager todoItemManager;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			#region Parse stuff
			todoItemManager = new TodoItemManager(ParseStorage.Default);

			App.SetTodoItemManager (todoItemManager);

			#endregion

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


