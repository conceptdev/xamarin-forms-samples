using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Xamarin.Forms.Xaml;

namespace dstemplate
{
    public partial class App : Xamarin.Forms.Application
    {
        public static MainPage _mainPage;
        public App()
        {
            InitializeComponent();
            AppTheme = "dark";
            _mainPage = new MainPage();
            MainPage = _mainPage;

            On<Windows>().SetImageDirectory("Assets");
        }

        public static string AppTheme { get; set; }

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
