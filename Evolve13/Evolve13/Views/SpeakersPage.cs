using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Evolve13
{
	public class SpeakersPage : ContentPage
	{
		ListView listView;
		public SpeakersPage ()
		{
			Title = "Sessions";
			Icon = "slideout.png";

			NavigationPage.SetHasNavigationBar (this, true);

			listView = new ListView {
				RowHeight = 40
			};
			listView.ItemsSource = App.Database.GetSpeakers ();
//			listView.ItemTemplate = new DataTemplate (typeof (ImageCell)){
//				Bindings = {
//					{ ImageCell.TextProperty, new Binding ("Name") },
//					{ ImageCell.ImageSourceProperty, new Binding ("HeadshotUrl") }
//				}
//			};
			listView.ItemTemplate = new DataTemplate (typeof (SpeakerCell));

			listView.ItemSelected += (sender, e) => {
				var speaker = e.SelectedItem as Speaker;
				var speakerPage = new SpeakerPage();
				speaker.HeadshotUrl = "https://evolve.xamarin.com/assets/images/pages/evolve/craig.jpg";
				speakerPage.BindingContext = speaker;
				Navigation.PushAsync(speakerPage);
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {listView}
			};
		}
	}
}