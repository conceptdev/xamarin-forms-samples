using FlagData;
using Xamarin.Forms;
using System;

namespace FlagFacts
{
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
            Device.OpenUri(vm.CurrentFlag.MoreInformationUrl);
        }

        private void OnPrevious(object sender, EventArgs e)
        {
            vm.MoveToPreviousFlag();
        }

        private void OnNext(object sender, EventArgs e)
        {
            vm.MoveToNextFlag();
        }
    }
}
