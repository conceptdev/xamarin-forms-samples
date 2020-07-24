using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlagFacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllFlagsView : ContentView
    {
        public event EventHandler<ItemTappedEventArgs> FlagTapped;

        public AllFlagsView()
        {
            InitializeComponent();
        }
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //await this.Navigation.PushAsync(new FlagDetailsPage());
            FlagTapped?.Invoke(sender, e);
        }
    }
}