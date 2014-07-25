using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public class MenuPage : ContentPage
	{
		MasterDetailPage master;

		TableView tableView;

		public MenuPage ()
		{
			Title = "LoginPattern";
			Icon = "slideout.png";

			var section = new TableSection () {
				new TextCell {Text = "Sessions"},
				new TextCell {Text = "Speakers"},
				new TextCell {Text = "Favorites"},
				new TextCell {Text = "Room Plan"},
				new TextCell {Text = "Map"},
			};

			var root = new TableRoot () {section} ;

			tableView = new TableView ()
			{ 
				Root = root,
				Intent = TableIntent.Menu,
			};

			var logoutButton = new Button { Text = "Logout" };
			logoutButton.Clicked += (sender, e) => {
				App.Logout();
			};

			Content = new StackLayout {
				BackgroundColor = Color.Gray,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					tableView, 
					logoutButton
				}
			};
		}


	}
}

