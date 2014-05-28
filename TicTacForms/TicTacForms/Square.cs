using System;
using Xamarin.Forms;

namespace TicTacForms
{
	class Square : ContentView
	{
		Label label;
		public string Text{
			get { return label.Text; }
			set { label.Text = value; }
		}
		public Square(string text)
		{
			label = new Label
			{
				Text = text,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			this.Padding = new Thickness(5);
			this.Content = new Frame
			{
				OutlineColor = Color.Accent,
				Content = label
			};

			// Don't let touch pass us by.
			this.BackgroundColor = Color.Transparent;
		}

		public int Row { set; get; }
		public int Col { set; get; }

		public Font Font
		{
			set { label.Font = value; }
		}
	}
}

