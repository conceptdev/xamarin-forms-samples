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
            if (!DeviceIsSpanned)
            {   // use Navigation
                await this.Navigation.PushAsync(new FlagDetailsPage());
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DualScreenInfo.Current.PropertyChanged += Current_PropertyChanged;
            DualScreenInfo.Current.HingeAngleChanged += Current_HingeAngleChanged;
            UpdateLayouts(); // for first page load
        }

        private void Current_HingeAngleChanged(object sender, HingeAngleChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Title = e.HingeAngleInDegrees.ToString();
            });
            
        }

        protected override void OnDisappearing()
        {
            DualScreenInfo.Current.PropertyChanged -= Current_PropertyChanged;
            base.OnDisappearing();
        }
        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;

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