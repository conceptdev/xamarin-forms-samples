using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Xamarin.Forms.Xaml;

namespace dstemplate
{
    public partial class App : Xamarin.Forms.Application
    {
        public static string AppTheme { get; set; }
        public static string FolderPath { get; private set; }

        public static MainPage _mainPage;

        public App()
        {
            InitializeComponent();

            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            AppTheme = "dark";
            _mainPage = new MainPage();
            MainPage = _mainPage;
        }

        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
