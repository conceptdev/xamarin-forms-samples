using System;
using Xamarin.Forms;

namespace Todo
{
	public static class Constants
	{
		public static Font Font {
			get {
				return Device.OnPlatform (
					Font.OfSize ("ChalkboardSE-Light", 20),  // ChalkboardSE-Light  or  MarkerFelt-Thin	
					Font.SystemFontOfSize (NamedSize.Medium),
					Font.SystemFontOfSize (NamedSize.Large)
				);
			}
		}

		public static Color Brown {
			get {
				return Color.FromHex ("a97946");
			}
		}

		public static Color Yellow {
			get {
				return Color.FromRgb (255,244,165);
			}
		}
	}
}

