using System;
using Xamarin.Forms;
using EmployeeDirectory.Data;
using EmployeeDirectory.ViewModels;

namespace EmployeeDirectory
{
	public partial class LoginXaml : ContentPage
	{
		LoginViewModel viewModel;
		ContentPage parent;

		public LoginXaml (ContentPage parent)
		{
			InitializeComponent ();	
			viewModel = new LoginViewModel ();
			this.parent = parent;
			BindingContext = viewModel;
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			if (App.Service == null) {
				App.Service = await MemoryDirectoryService.FromCsv ("XamarinDirectory.csv");
				viewModel.Service = App.Service;
			}
		}
		void OnLoginClicked (object sender, EventArgs e)
		{
			if (viewModel.CanLogin ()) {
				viewModel
				.LoginAsync (System.Threading.CancellationToken.None)
				.ContinueWith (_ => {
					App.LastUseTime = System.DateTime.UtcNow;
						parent.Title = "Employee Directory"; // remove " Login"
						Navigation.PopModalAsync ();
				});
				parent.Title = "Employee Directory"; // remove " Login"
				Navigation.PopModalAsync ();
			} else {
				DisplayAlert ("Error", viewModel.ValidationErrors, "OK");
			}
		}

		void OnHelpClicked (object sender, EventArgs e)
		{
			DisplayAlert ("Help", "Enter any username and password", "OK");
		}
	}
}

