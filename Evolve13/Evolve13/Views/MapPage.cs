using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Evolve13
{
	public class MapPage : ContentPage
	{
		public MapPage ()
		{
			NavigationPage.SetHasNavigationBar (this, true);

			Title = "Austin, Texas";

			/* DON'T FORGET
			 * Xamarin.QuickUIMaps.Init (); 
			 */
			var map = new Map(new MapSpan(new Position(30.26535, -97.738613), 0.05, 0.05))
			{
				MapType = MapType.Street,
				HeightRequest = 508
//				X=0, Y=0,
//				Width = 320,
//				Height = 420
			};
			map.BackgroundColor = Color.White;

			Pin pin;
			map.Pins.Add(pin = new Pin()
				{
					Label = "Evolve 2013", 
					Position = new Position(30.26535, -97.738613),
					Type = PinType.Place
				});

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					map

				}
			};
//			Content = new AbsoluteLayout {
//				//BackgroundColor = Color.Gray,
//				Children = {
//					map
//				}
//			};
		}
	}
}

