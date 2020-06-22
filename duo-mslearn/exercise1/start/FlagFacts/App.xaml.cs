using FlagData;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlagFacts
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<FlagDetailsViewModel>();

            InitializeComponent();

            MainPage = new NavigationPage(new AllFlagsPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
