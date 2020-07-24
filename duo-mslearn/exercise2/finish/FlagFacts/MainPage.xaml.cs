using FlagData;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.DualScreen;
using Xamarin.Forms.Xaml;

namespace FlagFacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        // can never be spanned when first viewed...
        bool wasSpanned = false;

        public MainPage()
        {
            BindingContext = DependencyService.Get<FlagDetailsViewModel>();
            InitializeComponent();
        }

        private async void ListView_FlagTapped(object sender, ItemTappedEventArgs e)
        {
            if (!DeviceIsSpanned && !DeviceIsBigScreen)
            {   // use Navigation only on phone-size single-screens
                await this.Navigation.PushAsync(new FlagDetailsPage());
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DualScreenInfo.Current.PropertyChanged += Current_PropertyChanged;
            UpdateLayouts(); // for first page load
        }
        protected override void OnDisappearing()
        {
            DualScreenInfo.Current.PropertyChanged -= Current_PropertyChanged;
            base.OnDisappearing();
        }
        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;
        public bool DeviceIsBigScreen => (Device.Idiom == TargetIdiom.Tablet) || (Device.Idiom == TargetIdiom.Desktop) ;

        private void Current_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"PropertyChanged: {e}");
            Console.WriteLine(DualScreenInfo.Current.SpanMode);
            Console.WriteLine("Length: " + DualScreenInfo.Current.SpanningBounds.Length);
            UpdateLayouts();
        }

        public async void UpdateLayouts()
        {
            Console.WriteLine($"DeviceIsSpanned: {DeviceIsSpanned}");
            if (DeviceIsSpanned || DeviceIsBigScreen)
            {
                twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
                twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
                wasSpanned = true;

                if (DeviceIsBigScreen)
                {   // layout 1/3 for list, 2/3 for details
                    twoPaneView.Pane1Length = new GridLength(1, GridUnitType.Star);
                    twoPaneView.Pane2Length = new GridLength(2, GridUnitType.Star);
                }
            }
            else
            {   // single-screen
                twoPaneView.PanePriority = TwoPaneViewPriority.Pane1;
                twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.SinglePane;
                twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;
                // wasSpanned check is needed, or this will open on first-run or rotation
                // stack count is needed, or we might push multiple on rotation
                if (wasSpanned && Navigation.NavigationStack.Count == 1)
                {   // open the detail page
                    await Navigation.PushAsync(new FlagDetailsPage());
                }
                wasSpanned = false;
            }
        }

        private void OnMoreInformation(object sender, EventArgs e)
        {
            Launcher.OpenAsync(((FlagDetailsViewModel)BindingContext).CurrentFlag.MoreInformationUrl);
        }
    }
}