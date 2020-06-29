using FlagData;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.DualScreen;

namespace FlagFacts
{
    public partial class AllFlagsPage : ContentPage
    {
        public AllFlagsPage()
        {
            BindingContext = DependencyService.Get<FlagDetailsViewModel>();
            InitializeComponent();
            DualScreenInfo.Current.PropertyChanged += Current_PropertyChanged;
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
            {
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