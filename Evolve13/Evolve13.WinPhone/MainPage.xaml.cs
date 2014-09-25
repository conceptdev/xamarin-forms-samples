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


namespace Evolve13.WinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();

            var sqliteFilename = "Evolve.db3";
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename); ;

            var plat = new SQLite.Net.Platform.WindowsPhone8.SQLitePlatformWP8();
            var conn = new SQLite.Net.SQLiteConnection(plat, path);

            // Set the database connection string
            Evolve13.App.SetDatabaseConnection(conn);


            Content = Evolve13.App.GetMainPage().ConvertPageToUIElement(this);
        }
    }
}
