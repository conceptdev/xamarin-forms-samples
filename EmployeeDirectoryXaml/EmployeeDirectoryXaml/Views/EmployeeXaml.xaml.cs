using System;
using Xamarin.Forms;
using EmployeeDirectory.Data;
using EmployeeDirectory.ViewModels;
using EmployeeDirectory.Utilities;

namespace EmployeeDirectory
{
	public partial class EmployeeXaml : ContentPage
	{
		IPhoneFeatureService PhoneFeatureService;

		public EmployeeXaml () {
			InitializeComponent ();	

			NameLabel.Font = Font.BoldSystemFontOfSize (NamedSize.Large);

			PhoneFeatureService = DependencyService.Get<IPhoneFeatureService> ();
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			GetImage ();
		}

		void OnFavoriteClicked (object sender, EventArgs e) {
			var p = (PersonViewModel)BindingContext;
			p.ToggleFavorite ();
			this.Navigation.PopAsync();
		}

		void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {

			var property = (PersonViewModel.Property)e.SelectedItem;
			System.Diagnostics.Debug.WriteLine ("Property clicked " + property.Type + " " + property.Value);

			switch (property.Type) {
			case PersonViewModel.PropertyType.Email:
				// use OpenUri
//				Device.OpenUri (new Uri ("mailto:"+property.Value));
				// or use platform-specific code
				PhoneFeatureService.Email (property.Value);
				break;
			case PersonViewModel.PropertyType.Twitter:
				PhoneFeatureService.Tweet (property.Value);
				break;
			case PersonViewModel.PropertyType.Url:
				PhoneFeatureService.Browse (property.Value);
				break;
			case PersonViewModel.PropertyType.Phone:
				PhoneFeatureService.Call (property.Value);
				break;
			case PersonViewModel.PropertyType.Address:
				PhoneFeatureService.Map (property.Value);
				break;
			}
		}

		void OnCancelClicked (object sender, EventArgs e) {
			this.Navigation.PopAsync();
		}

		const int ImageSize = 88*2; //176

		void GetImage () {
			var p = (PersonViewModel)BindingContext;
			var person = p.Person;

			if (person.HasEmail) {
				var imageUrl = Gravatar.GetImageUrl (person.Email, ImageSize);

				var loader = new UriImageSource ();
				loader.Uri = imageUrl;
				PersonImage.Source = loader;
			}
		}
	}
}