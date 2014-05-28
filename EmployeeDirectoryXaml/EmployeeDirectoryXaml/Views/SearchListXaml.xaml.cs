using System;
using Xamarin.Forms;
using System.Linq;
using EmployeeDirectory.Data;
using EmployeeDirectory.ViewModels;

namespace EmployeeDirectory
{
	public partial class SearchListXaml : ContentPage
	{
		Search search;

		public SearchListXaml ()
		{
			InitializeComponent ();

			favoritesRepository = XmlFavoritesRepository.OpenFile ("XamarinFavorites.xml").Result;

			search = new Search ("test");
			viewModel = new SearchViewModel (App.Service, search);

			viewModel.SearchCompleted += (sender, e) => {
				if (viewModel.Groups == null) {
					// clear it out
					listView.ItemsSource = new string [1];
				} else {
					listView.ItemsSource = viewModel.Groups;
				}
			};
			viewModel.Error += (sender, e) => {
				//	e.Exception
			};
			BindingContext = viewModel; 
		}

		SearchViewModel viewModel;
		IFavoritesRepository favoritesRepository;

		void OnValueChanged (object sender, EventArgs e) {
			// perform search on each keypress
			viewModel.Search ();
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