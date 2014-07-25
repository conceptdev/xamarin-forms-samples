using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using EmployeeDirectory;

namespace EmployeeDirectory.Android
{
	[Activity (Label = "Employee Directory", MainLauncher = true)]
	public class Activity1 : Xamarin.Forms.Platform.Android.AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			#region Copy static data into working folder
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder

			var path = Path.Combine(documentsPath, "XamarinDirectory.csv");
			//Console.WriteLine (path);
			if (!File.Exists (path)) {
				var s = Resources.OpenRawResource(EmployeeDirectory.Android.Resource.Raw.XamarinDirectory);  // RESOURCE NAME ###
				// create a write stream
				FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
				// write to the stream
				ReadWriteStream(s, writeStream);
			}
			path = Path.Combine(documentsPath, "XamarinFavorites.xml");
			//Console.WriteLine (path);
			if (!File.Exists (path)) {
				var s = Resources.OpenRawResource(EmployeeDirectory.Android.Resource.Raw.XamarinFavorites);  // RESOURCE NAME ###
				// create a write stream
				FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
				// write to the stream
				ReadWriteStream(s, writeStream);
			}
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

		public override bool OnKeyDown (Keycode keyCode, KeyEvent e)
		{          
			Console.WriteLine ("OnKeyDown:" + this.ActionBar.Title);
			if (keyCode == Keycode.Back) 
			{
				if (this.ActionBar.Title != null && this.ActionBar.Title.Contains("Login")) {
					// The ROOT page is initially set to have 'Login' in .Title
					// when the app starts (ie. it's hardcoded).
					// If we're on the login page, swallow the back button.
					// Note that when login occurs successfully, the .Title
					// of the page is changed so that the back button works.
					return false;
				}
			}
			return base.OnKeyDown (keyCode, e);
		}

		public override void OnBackPressed ()
		{
			// could do something in this method instead of OnKeyDown above
			base.OnBackPressed ();
		}
	}
}


