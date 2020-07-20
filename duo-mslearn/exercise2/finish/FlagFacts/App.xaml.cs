using FlagData;
using Xamarin.Forms;

namespace FlagFacts
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<FlagDetailsViewModel>();

            InitializeComponent();

            // TODO: if not dual-screen device, use old nav style
            //MainPage = new NavigationPage(new AllFlagsPage());
            // TODO: else use dual-screen mainpage
            MainPage = new NavigationPage(new MainPage());
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
