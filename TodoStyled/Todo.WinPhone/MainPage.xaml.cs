using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Xamarin.Forms;
using System.IO;
using Windows.Storage;
using System.Diagnostics;


namespace Todo.WinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();
            try
            {
                var sqliteFilename = "TodoSQLite.db3";
                var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename); ;

                var plat = new SQLite.Net.Platform.WindowsPhone8.SQLitePlatformWP8();
                var conn = new SQLite.Net.SQLiteConnection(plat, path);

                // Set the database connection string
                Todo.App.SetDatabaseConnection(conn);

                Todo.App.SetTextToSpeech(new TextToSpeech_WinPhone());

                Content = Todo.App.GetMainPage().ConvertPageToUIElement(this);
            }
            catch (Exception e) {
                Debug.WriteLine(e);
            }
        }
    }
}
