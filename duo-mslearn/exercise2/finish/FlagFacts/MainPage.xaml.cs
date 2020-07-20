using FlagData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.DualScreen;
using Xamarin.Forms.Xaml;

namespace FlagFacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = DependencyService.Get<FlagDetailsViewModel>();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DualScreenInfo.Current.PropertyChanged += Current_PropertyChanged;
        }
        protected override void OnDisappearing()
        {
            //unsub
            base.OnDisappearing();
        }
        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;

        private void Current_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"PropertyChanged: {e}");
            Console.WriteLine(DualScreenInfo.Current.SpanMode);
            Console.WriteLine(DualScreenInfo.Current.SpanningBounds);
            UpdateLayouts();
        }

        public void UpdateLayouts()
        {
            Console.WriteLine($"DeviceIsSpanned: {DeviceIsSpanned}");
            if (DeviceIsSpanned)
            {
                twoPaneView.PanePriority = TwoPaneViewPriority.Pane1; // no-op??
                twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
                twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
            }
            else
            {   // single-screen
                twoPaneView.PanePriority = TwoPaneViewPriority.Pane2;
                twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.SinglePane;
                twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;
            }
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            twoPaneView.PanePriority = TwoPaneViewPriority.Pane2;
        }
        protected override bool OnBackButtonPressed()
        {
            if (twoPaneView.PanePriority == TwoPaneViewPriority.Pane2)
            {
                twoPaneView.PanePriority = TwoPaneViewPriority.Pane1;
                return true;
            }
            return false;
        }

        private void OnMoreInformation(object sender, EventArgs e)
        {
            Launcher.OpenAsync(((FlagDetailsViewModel)BindingContext).CurrentFlag.MoreInformationUrl);
        }
    }
}