using FlagData;
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

        private async void ListView_FlagTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new FlagDetailsPage());
        }
    }
}