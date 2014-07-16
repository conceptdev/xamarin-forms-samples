using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Evolve13
{
	public class FavoritesPage : ContentPage
	{
		ListView listView;
		public FavoritesPage ()
		{
			Title = "Favorites";

			NavigationPage.SetHasNavigationBar (this, true);

			listView = new ListView {
				RowHeight = 40
			};
			listView.ItemsSource = new Session [] { new Session {Title = "test", Location="somewhere"} };
			listView.ItemTemplate = new DataTemplate (typeof (SessionCell));

			listView.ItemSelected += (sender, e) => {
				var session = e.SelectedItem;
				var sessionPage = new SessionPage();
				//todoPage.BindingContext = todoItem;
				Navigation.PushAsync(sessionPage);
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {listView}
			};
		}
	}
}