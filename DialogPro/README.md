DialogPro
=========

This is a (currently, very basic) example of how Xamarin.Forms can be used to express similar UIs to [MonoTouch.Dialog](https://github.com/migueldeicaza/MonoTouch.Dialog). This is the main part of the code that is running to make the screenshots below:

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

![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/DialogPro/Screenshots/iOS.png "iOS")

![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/DialogPro/Screenshots/Android.png "Android")
