using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Roget1911
{
	public class MainListPage : ContentPage
	{
		ListView listView;
		public MainListPage ()
		{
			Title = "Roget's 1911";
			NavigationPage.SetHasNavigationBar (this, true);
			listView = new ListView { RowHeight = 40 };
			
			listView.ItemTemplate = new DataTemplate (typeof (TextCell)) {
				Bindings = {
					{ TextCell.TextProperty, new Binding ("Name") }
				}
			};

			listView.ItemSelected += (sender, e) => {
				var @class = (RogetClass)e.SelectedItem;
				var categoryPage = new SectionListPage();
				categoryPage.Title = @class.Name;
				categoryPage.BindingContext = @class.Sections;
				Navigation.PushAsync(categoryPage);
			};

			// HACK: workaround issue #894 for now
			if (Device.OS == TargetPlatform.iOS)
				listView.ItemsSource = new string [1] {""};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {listView}
			};
		}


		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			await App.LoadXml ();
			listView.ItemsSource = App.XmlData.Classes;
		}
	}
}

