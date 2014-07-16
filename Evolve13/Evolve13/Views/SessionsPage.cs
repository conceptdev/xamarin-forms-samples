using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Evolve13
{
	public class SessionsPage : ContentPage
	{
		ListView listView;
		public SessionsPage ()
		{
			Title = "Sessions";

			NavigationPage.SetHasNavigationBar (this, true);

			listView = new ListView {
				RowHeight = 40
			};
			// see the SessionCell implementation for how the variable row height is calculated
			listView.HasUnevenRows = true;

			listView.ItemsSource = App.Database.GetSessions ();
			listView.ItemTemplate = new DataTemplate (typeof (SessionCell));

			listView.ItemSelected += (sender, e) => {
				var session = e.SelectedItem as Session;
				var sessionPage = new SessionPage();
				sessionPage.BindingContext = session;
				Navigation.PushAsync(sessionPage);
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {listView}
			};
		}
	}
}