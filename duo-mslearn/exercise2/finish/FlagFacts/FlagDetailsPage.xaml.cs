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
    public partial class FlagDetailsPage : ContentPage
    {
        private readonly FlagDetailsViewModel vm;

        public FlagDetailsPage()
        {
            vm = DependencyService.Get<FlagDetailsViewModel>();

            InitializeComponent();
            BindingContext = vm;
        }

        private async void OnShow(object sender, EventArgs e)
        {
            vm.CurrentFlag.DateAdopted = vm.CurrentFlag.DateAdopted.AddYears(1);
            await DisplayAlert(vm.CurrentFlag.Country,
                $"{vm.CurrentFlag.DateAdopted:D} - {vm.CurrentFlag.IncludesShield}: {vm.CurrentFlag.MoreInformationUrl}",
                "OK");
        }

        private void OnMoreInformation(object sender, EventArgs e)
        {
            Launcher.OpenAsync(((FlagDetailsViewModel)BindingContext).CurrentFlag.MoreInformationUrl);
        }

        private void OnPrevious(object sender, EventArgs e)
        {
            vm.MoveToPreviousFlag();
        }

        private void OnNext(object sender, EventArgs e)
        {
            vm.MoveToNextFlag();
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

        private void Current_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"PropertyChanged: {e}");
            Console.WriteLine(DualScreenInfo.Current.SpanMode);
            Console.WriteLine("Length: " + DualScreenInfo.Current.SpanningBounds.Length);
            UpdateLayouts();
        }
        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;
        public bool DeviceIsBigScreen => (Device.Idiom == TargetIdiom.Tablet) || (Device.Idiom == TargetIdiom.Desktop);

        public async void UpdateLayouts()
        {
            Console.WriteLine($"DeviceIsSpanned: {DeviceIsSpanned}");
            if (DeviceIsSpanned)
            {  // the detail view should never be showing when spanned
               if (Navigation.NavigationStack.Count > 1)
               {
                    await Navigation.PopToRootAsync();
               }
            }
        }
    }
}