using System;
using Xamarin.Forms;

namespace Evolve13
{
	public class SpeakerCell : ViewCell
	{
		Label title, label;
		Image photo, mask;
		StackLayout layout;
		Grid photoGrid;

		public SpeakerCell ()
		{
			title = new Label {
				YAlign = TextAlignment.Center
			};
			title.SetBinding (Label.TextProperty, "Title");

			label = new Label {
				YAlign = TextAlignment.Center,
				Font = Font.SystemFontOfSize(10)
			};
			label.SetBinding (Label.TextProperty, "LocationDisplay");

			photo = new Image {
				WidthRequest = 38, HeightRequest = 38,
			};
			mask = new Image {
				Source = "roundmask.png",
				WidthRequest = 38, HeightRequest = 38, 
			};
			photoGrid = new Grid {
				ColumnDefinitions = { new ColumnDefinition () },
				RowDefinitions = { new RowDefinition () }
			};
			photoGrid.Children.Add (photo);
			photoGrid.Children.Add (mask);


			var text = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(0, 0, 0, 0),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {title, label}
			};

			layout = new StackLayout {
				Padding = new Thickness(10, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {photoGrid, text}
			};
			View = layout;
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			var speaker = (Speaker)BindingContext;

			title.Text = speaker.Name;
			label.Text = speaker.TwitterHandle;
			try {
				photo.Source = ImageSource.FromUri (new Uri(speaker.HeadshotUrl));
				System.Diagnostics.Debug.WriteLine ("++ " + speaker.HeadshotUrl);
			} catch {
				photo.Source = "Icon";
				System.Diagnostics.Debug.WriteLine ("-- " + speaker.HeadshotUrl);
			}

		}
	}
}

