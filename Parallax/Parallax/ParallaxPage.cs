using System;

using Xamarin.Forms;

namespace Parallax
{
	/*
	 *      WE RECOMMEND YOU TEST ON DEVICE
	 */
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

			// grid is used to position the labels ON TOP OF the image
			var layeringGrid = new Grid ();

			var outerScrollView = new ScrollView ();
			outerScrollView.Scrolled += (object sender, ScrolledEventArgs e) => {
				var imageHeight = img.Height * 2;
				var scrollRegion = layeringGrid.Height - outerScrollView.Height;
				var parallexRegion = imageHeight - outerScrollView.Height;
				img.TranslationY = outerScrollView.ScrollY - parallexRegion * (outerScrollView.ScrollY / scrollRegion);
			};

			outerScrollView.Content = layeringGrid;

			layeringGrid.Children.Add (img);

			StackLayout stackOfLabels = new StackLayout ();

			for (int i = 0; i < parks.Length; i++)
			{
				stackOfLabels.Children.Add (new Label () { Text = parks[i], HeightRequest = 50, 
					FontSize = Device.OnPlatform (iOS: 30, Android:Device.GetNamedSize(NamedSize.Large, typeof(Label)), WinPhone:Device.GetNamedSize(NamedSize.Default, typeof(Label)) ),
					TextColor = Device.OnPlatform(iOS:Color.White, Android:Color.White, WinPhone:Color.Default),
					FontAttributes = FontAttributes.Bold
				});
			}

			layeringGrid.Children.Add (stackOfLabels);

			// The root page of your application    
			Content = outerScrollView;
			/*
			The visual tree will look something like this:
	
			* Content
			    + ScrollView
			       + Grid
			          + StackLayout
					     + Label(s)
			          + Image

			When the scrollview is scrolled, the image is translated manually
			*/

		}
	
		//http://en.wikipedia.org/wiki/List_of_national_parks_of_the_United_States
		string[] parks = new string[] {"Acadia"
			,"American Samoa"
			,"Arches"
			,"Badlands"
			,"Big Bend"
			,"Biscayne"
			,"Black Canyon of the Gunnison"
			,"Bryce Canyon"
			,"Canyonlands"
			,"Capitol Reef"
			,"Carlsbad Caverns"
			,"Channel Islands"
			,"Congaree"
			,"Crater Lake"
			,"Cuyahoga Valley"
			,"Death Valley"
			,"Denali"
			,"Dry Tortugas"
			,"Everglades"
			,"Gates of the Arctic"
			,"Glacier"
			,"Glacier Bay"
			,"Grand Canyon"
			,"Grand Teton"
			,"Great Basin"
			,"Great Sand Dunes"
			,"Great Smoky Mountains"
			,"Guadalupe Mountains"
			,"Haleakalā"
			,"Hawaii Volcanoes"
			,"Hot Springs"
			,"Isle Royale"
			,"Joshua Tree"
			,"Katmai"
			,"Kenai Fjords"
			,"Kings Canyon"
			,"Kobuk Valley"
			,"Lake Clark"
			,"Lassen Volcanic"
			,"Mammoth Cave"
			,"Mesa Verde"
			,"Mount Rainier"
			,"North Cascades"
			,"Olympic"
			,"Petrified Forest"
			,"Pinnacles"
			,"Redwood"
			,"Rocky Mountain"
			,"Saguaro"
			,"Sequoia"
			,"Shenandoah"
			,"Theodore Roosevelt"
			,"Virgin Islands"
			,"Voyageurs"
			,"Wind Cave"
			,"Wrangell –St. Elias"
			,"Yellowstone"
			,"Yosemite"
			,"Zion"};
	}
}


