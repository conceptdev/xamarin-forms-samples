using System;
using Xamarin.Forms;

namespace BugSweeper
{
	public class App : Application
    {
        public App()
        {
            MainPage = new BugSweeperPage();

			Xamarin.Insights.Identify(Xamarin.Insights.Traits.GuestIdentifier, null);
        }
    }
}
