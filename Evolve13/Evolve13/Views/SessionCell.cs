using System;
using Xamarin.Forms;

namespace Evolve13
{
	public class SessionCell : ViewCell
	{
		Label title, label;
		StackLayout layout;
		public SessionCell ()
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

			var fav = new Image {
				Source = FileImageSource.FromFile ("favorite.png"),
			};
			//TODO: implement favorites
			//fav.SetBinding (Image.IsVisibleProperty, "IsFavorite");

			var text = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(0, 0, 0, 0),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {title, label}
			};

			layout = new StackLayout {
				Padding = new Thickness(20, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {text, fav}
			};
			View = layout;
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			var session = (Session)BindingContext;

			// rough translation of character-count to cell height
			// doesn't always work, but close enough for now
			if (session.Title.Length > 75)
				this.Height = 110;
			else if (session.Title.Length > 60)
				this.Height = 80; 
			else if (session.Title.Length > 30)
				this.Height = 60;
			else
				this.Height = 40;
		}
	}
}

