using System;
using Xamarin.Forms;
using System.Linq;

namespace DialogPro
{
	public class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			var svm = new SettingsViewModel { 
				AirplaneMode = true
			};
			BindingContext = svm;

			var airplaneModeCell = new SwitchCell {Text = "Airplane Mode"};
			airplaneModeCell.SetBinding(SwitchCell.OnProperty, "AirplaneMode");

			Title = "Settings";
			Content = new TableView {
				Root = new TableRoot {
					new TableSection (" ") {
						airplaneModeCell,
						new SwitchCell {Text = "Notifications"}
					},
					new TableSection (" ") {
						new EntryCell { Label="Login", Placeholder = "username" }
						, new EntryCell {Label="Password", Placeholder = "password" }
					},
					new TableSection ("Silent") {
						new SwitchCell {Text = "Vibrate", },
						new ViewCell { View = new Slider() }
					},
				
					new TableSection ("Ring") {
						new SwitchCell {Text = "New Voice Mail"},
						new SwitchCell {Text = "New Mail", On = true}
					},
				},
			};
		}
	}
}

