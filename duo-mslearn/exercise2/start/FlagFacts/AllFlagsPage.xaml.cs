using FlagData;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FlagFacts
{
    public partial class AllFlagsPage : ContentPage
    {
        public AllFlagsPage()
        {
            BindingContext = DependencyService.Get<FlagDetailsViewModel>();
            InitializeComponent();
        }

        private void OnMoreInformation(object sender, EventArgs e)
        {
            Launcher.OpenAsync(((FlagDetailsViewModel)BindingContext).CurrentFlag.MoreInformationUrl);
        }
    }
}