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
        // can never be Spanned when first viewed...
        bool wasSpanned = false;

        public MainPage()
        {
            BindingContext = DependencyService.Get<FlagDetailsViewModel>();
            InitializeComponent();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (DeviceIsSpanned)
            {   // no-op
                twoPaneView.PanePriority = TwoPaneViewPriority.Pane2;
            }
            else
            {   // use Navigation
                await this.Navigation.PushAsync(new FlagDetailsPage());
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DualScreenInfo.Current.PropertyChanged += Current_PropertyChanged;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DualScreenInfo.Current.PropertyChanged -= Current_PropertyChanged;
        }
        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;

        private void Current_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"PropertyChanged: {e}");
            Console.WriteLine(DualScreenInfo.Current.SpanMode);
            Console.WriteLine(DualScreenInfo.Current.SpanningBounds);
            UpdateLayouts();
        }

        public async void UpdateLayouts()
        {
            Console.WriteLine($"DeviceIsSpanned: {DeviceIsSpanned}");
            if (DeviceIsSpanned)
            {
                twoPaneView.PanePriority = TwoPaneViewPriority.Pane1; // no-op??
                twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
                twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
                wasSpanned = true;
            }
            else
            {   // single-screen
                if (wasSpanned)
                {
                    await this.Navigation.PushAsync(new FlagDetailsPage());
                }
                twoPaneView.PanePriority = TwoPaneViewPriority.Pane1;
                twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.SinglePane;
                twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;
                wasSpanned = false;
            }
        }

        private void OnMoreInformation(object sender, EventArgs e)
        {
            Launcher.OpenAsync(((FlagDetailsViewModel)BindingContext).CurrentFlag.MoreInformationUrl);
        }
    }
}