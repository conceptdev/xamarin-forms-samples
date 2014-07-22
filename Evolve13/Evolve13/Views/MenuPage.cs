using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Evolve13
{
	/// <summary>
	/// Required for PlatformRenderer
	/// </summary>
	public class MenuTableView : TableView 
	{
	}

	public class MenuPage : ContentPage
	{
		MasterDetailPage master;

		TableView tableView;

		public MenuPage (MasterDetailPage m)
		{
			master = m;

			Title = "Evolve13";
			Icon = "slideout.png";

			var section = new TableSection () {
				new MenuCell {Text = "Sessions", Host = this},
				new MenuCell {Text = "Speakers", Host = this},
				new MenuCell {Text = "Favorites", Host = this},
				new MenuCell {Text = "Room Plan", Host = this},
				new MenuCell {Text = "Map", Host = this},
				new MenuCell {Text = "About", Host = this},
			};

			var root = new TableRoot () {section} ;
		
			tableView = new MenuTableView ()
			{ 
				Root = root,
//				HeaderTemplate = new DataTemplate (typeof(MenuHeader)),
				Intent = TableIntent.Menu,
			};


			Content = new StackLayout {
				BackgroundColor = Color.Gray,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {tableView}
			};
		}

		NavigationPage sessions, speakers, favorites;
		public void Selected (string item) {

			switch (item) {
			case "Sessions":
				if (sessions == null)
					sessions = new NavigationPage (new SessionsPage ()) { Tint = App.NavTint };
				master.Detail = sessions;
				break;
			case "Speakers":
				if (speakers == null) {
					speakers = new NavigationPage (new SpeakersPage ()) { Tint = App.NavTint };
					//TODO: finish WrapLayout demo
//					speakers = new NavigationPage (new SpeakersPageWrap ()) { Tint = App.NavTint };
				}
				master.Detail = speakers;
				break;
			case "Favorites":
				if (favorites == null)
					favorites = new NavigationPage (new FavoritesPage ()) { Tint = App.NavTint };
				master.Detail = favorites;
				break;
			case "Room Plan":
				master.Detail = new NavigationPage(new FloorplanPage()) {Tint = App.NavTint};
				break;
			case "Map":
				master.Detail = new NavigationPage(new MapPage()) {Tint = App.NavTint};
				break;
			case "About":
				master.Detail = new NavigationPage(new AboutPage()) {Tint = App.NavTint};
				break;
			};
			master.IsPresented = false;  // close the slide-out
		}
	}
}