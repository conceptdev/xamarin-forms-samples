using System;
using Xamarin.Forms;

namespace Evolve13
{
	public class MenuCell : ViewCell
	{
		public string Text { 
			get { return label.Text; }
			set{ label.Text = value;} 
		}
		Label label;

		public MenuPage Host { get; set; }

		public MenuCell ()
		{
			label = new Label {
				YAlign = TextAlignment.Center,
				TextColor = Color.White,
			};

			var layout = new StackLayout {
				BackgroundColor = App.HeaderTint,
				Padding = new Thickness(20, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {label}
			};
			View = layout;
		}

		protected override void OnTapped ()
		{
			base.OnTapped ();

			Host.Selected (label.Text);
		}
	}
}

