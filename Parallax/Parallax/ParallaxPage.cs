using System;

using Xamarin.Forms;

namespace Parallax
{
	public class ParallaxPage : ContentPage
	{
		public ParallaxPage ()
		{
			var img = new Image () {
				Source = "https://c4.staticflickr.com/8/7372/16353550400_ce43734d93_b.jpg", 
				VerticalOptions = LayoutOptions.Start, 
				Scale = 2, 
				AnchorY = 0
			};

			var g = new Grid ();
			var s = new ScrollView ();
			s.Scrolled += (object sender, ScrolledEventArgs e) => {
				var imageHeight = img.Height * 2;
				var scrollRegion = g.Height - s.Height;
				var parallexRegion = imageHeight - s.Height;
				img.TranslationY = s.ScrollY - parallexRegion * (s.ScrollY / scrollRegion);
			};


			s.Content = g;

			g.Children.Add (img);

			StackLayout l = new StackLayout ();

			for (int i = 0; i < 50; i++)
			{
				l.Children.Add (new Label () { Text = "ewljrjwel weljrlkewjrw lkjrlkwejrlkwe", HeightRequest = 50 });
			}

			g.Children.Add (l);

			// The root page of your application    
			Content = s;
		}
	}
}


