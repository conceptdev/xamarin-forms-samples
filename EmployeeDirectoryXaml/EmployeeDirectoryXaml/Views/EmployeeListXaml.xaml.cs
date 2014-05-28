using System;
using Xamarin.Forms;
using System.Linq;
using EmployeeDirectory.Data;
using EmployeeDirectory.ViewModels;

namespace EmployeeDirectory
{
	public partial class EmployeeListXaml : ContentPage
	{
		public EmployeeListXaml ()
		{
			InitializeComponent ();

			var tbi = new ToolbarItem ("?", "Search.png", () => {
				// search page
				var search = new SearchListXaml();
				Navigation.PushAsync(search);
			}, 0, 0);

			ToolbarItems.Add (tbi);
		}

		FavoritesViewModel viewModel;
		IFavoritesRepository favoritesRepository;

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();

			if (LoginViewModel.ShouldShowLogin (App.LastUseTime)) {
				Navigation.PushModalAsync (new LoginXaml ());
			}

			//
			// Load the favorites
			//
			favoritesRepository = await XmlFavoritesRepository.OpenIsolatedStorage ("XamarinFavorites.xml");

			if (favoritesRepository.GetAll ().Count () == 0) {
				favoritesRepository = await XmlFavoritesRepository.OpenFile ("XamarinFavorites.xml");
			}

			viewModel = new FavoritesViewModel (favoritesRepository, false);

			listView.ItemsSource = viewModel.Groups;
		}

		public void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {
			var p = e.SelectedItem as Person;
			var em = new EmployeeXaml();

			var pvm = new PersonViewModel (p, favoritesRepository);
			em.BindingContext = pvm;
			Navigation.PushAsync(em);
		}
	}
}