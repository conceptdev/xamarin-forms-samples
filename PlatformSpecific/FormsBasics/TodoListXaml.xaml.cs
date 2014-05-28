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

			// HACK: workaround issue #894 for now
			if (Device.OS == TargetPlatform.iOS)
				listView.ItemsSource = new string [1] {""};

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