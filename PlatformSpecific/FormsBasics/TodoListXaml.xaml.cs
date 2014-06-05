using System;
using Xamarin.Forms;

// xmlns="http://xamarin.com/schemas/2014/forms"

namespace PlatformSpecific
{
	public partial class TodoListXaml : ContentPage
	{
		public TodoListXaml ()
		{
			InitializeComponent ();

			// same as the Xaml, but in code
			this.Padding = 
				new Thickness(5, Device.OnPlatform(20, 0, 0), 0, 0);

			var tbi = new ToolbarItem ("+", null, () => {
				//TODO: something
			}, 0, 0);
			if (Device.OS == TargetPlatform.Android) { // BUG: Android doesn't support the icon being null
				//TODO: something
			}

			ToolbarItems.Add (tbi);

			listView.ItemsSource = new TodoItem [] { 
				new TodoItem {Name = "Buy pears`"}, 
				new TodoItem {Name = "Buy oranges`", Done=true},
				new TodoItem {Name = "Buy mangos`"}, 
				new TodoItem {Name = "Buy apples`", Done=true},
				new TodoItem {Name = "Buy bananas`", Done=true}
			};

			if (Device.OS == TargetPlatform.iOS) {
				var tbi2 = new ToolbarItem ("?", null, () => {
					//TODO: something
				}, 0, 0);
				ToolbarItems.Add (tbi2);
			}
		}

		// ItemSelected="OnItemSelected"
//		public void OnItemSelected (object sender, EventArgs e) {
//			var todoItem = e.Data as TodoItem;
//			// TODO: something
//		}
	}
}