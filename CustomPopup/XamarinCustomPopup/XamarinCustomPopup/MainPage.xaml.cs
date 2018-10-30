using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinCustomPopup
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            List<string> items = new List<string> { "Xamarin.Forms", "Xamarin.iOS", "Xamarin.Android","Xamarin Monkeys" };
            imgMonkey.Source = ImageSource.FromResource("XamarinCustomPopup.images.monkey-MVP.png");
            imgPopup.Source = ImageSource.FromResource("XamarinCustomPopup.images.xammonkey.png");
            sampleList.ItemsSource = items;
        }

        private void btnPopupButton_Clicked(object sender, EventArgs e)
        {
            popupImageView.IsVisible = true;
            activityIndicator.IsRunning = true;
           
        }

       
    }
}
