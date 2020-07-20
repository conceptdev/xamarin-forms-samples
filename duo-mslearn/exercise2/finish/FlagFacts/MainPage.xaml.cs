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
            DualScreenInfo.Current.PropertyChanged += Current_PropertyChanged;
            InitializeComponent();
        }

        private async void ListView_FlagTapped(object sender, ItemTappedEventArgs e)
        {
            if (DeviceIsSpanned)
            {   // no-op?
                // uses databinding
            }
            else
            {
                if (DualScreenInfo.Current.IsLandscape)
                {
                    // no-op?
                    // uses databinding
                }
                else {
                    // use Navigation
                    await this.Navigation.PushAsync(new FlagDetailsPage());
                }
            }
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    DualScreenInfo.Current.PropertyChanged += Current_PropertyChanged;
        //}
        //protected override void OnDisappearing()
        //{
        //    DualScreenInfo.Current.PropertyChanged -= Current_PropertyChanged;
        //    base.OnDisappearing();
        //}
        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;
        //public bool DeviceIsSpanned => DualScreenInfo.Current.SpanningBounds.Length > 0;

        private void Current_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"PropertyChanged: {e}");
            Console.WriteLine(DualScreenInfo.Current.SpanMode);
            Console.WriteLine("Length: " + DualScreenInfo.Current.SpanningBounds.Length);
            UpdateLayouts();
        }

        public void UpdateLayouts()
        {
            Console.WriteLine($"DeviceIsSpanned: {DeviceIsSpanned}");
            if (DeviceIsSpanned)
            {
                twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
                twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
                wasSpanned = true;
            }
            else
            {   // single-screen
                twoPaneView.PanePriority = TwoPaneViewPriority.Pane1;
                twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.SinglePane;
                twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;
                //if (wasSpanned)
                //{
                ////    await this.Navigation.PushAsync(new FlagDetailsPage());
                //}

                //if (DualScreenInfo.Current.IsLandscape)
                //{
                //    twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
                //}
                //else
                //{
                //    twoPaneView.PanePriority = TwoPaneViewPriority.Pane1;
                //    twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.SinglePane;
                //    twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;
                //}
                wasSpanned = false;
            }
        }

        private void OnMoreInformation(object sender, EventArgs e)
        {
            Launcher.OpenAsync(((FlagDetailsViewModel)BindingContext).CurrentFlag.MoreInformationUrl);
        }
    }
}