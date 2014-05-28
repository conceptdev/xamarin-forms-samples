using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Evolve13
{
	public class SessionPage : ContentPage
	{
		public SessionPage ()
		{
			Title = "Session";

			NavigationPage.SetHasNavigationBar (this, true);

			var title = new Label { 
				Text = "Session",
				Font = Font.BoldSystemFontOfSize(18)
			};
			title.SetBinding (Label.TextProperty, "Title");

			var time = new Label { 
				Text = "Time",
				Font = Font.SystemFontOfSize(10)
			};
			time.SetBinding (Label.TextProperty, "DateTimeDisplay");

			var location = new Label { 
				Text = "Location",
				Font = Font.SystemFontOfSize(10)
			};
			location.SetBinding (Label.TextProperty, "LocationDisplay");

			var @abstract = new Label { 
				Text = "Brief",
				Font = Font.SystemFontOfSize(12)
			};
			@abstract.SetBinding (Label.TextProperty, "Abstract");


			var scroll = new ScrollView { 
				Orientation = ScrollOrientation.Vertical,
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.StartAndExpand,
					Padding = new Thickness(20),
					Children = {
						title, 
						time,
						location,
						@abstract,
					}
				}
			};
			Content = scroll;
		}
	}
}