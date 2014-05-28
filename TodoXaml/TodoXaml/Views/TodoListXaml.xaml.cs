using System;
using Xamarin.Forms;

// http://forums.xamarin.com/discussion/11279/quickui-xaml-gets-started/

namespace TodoXaml
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
				var todoItem = new TodoItem();
				var todoPage = new TodoItemXaml();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
			}, 0, 0);
			if (Device.OS == TargetPlatform.Android) { // BUG: Android doesn't support the icon being null
				tbi = new ToolbarItem ("+", "plus", () => {
					var todoItem = new TodoItem();
					var todoPage = new TodoItemXaml();
					todoPage.BindingContext = todoItem;
					Navigation.PushAsync(todoPage);
				}, 0, 0);
			}

			ToolbarItems.Add (tbi);

			if (Device.OS == TargetPlatform.iOS) {
				var tbi2 = new ToolbarItem ("?", null, () => {
					var todos = App.Database.GetItemsNotDone();
					var tospeak = "";
					foreach (var t in todos)
						tospeak += t.Name + " ";
					if (tospeak == "") tospeak = "there are no tasks to do";
					App.Speech.Speak(tospeak);
				}, 0, 0);
				ToolbarItems.Add (tbi2);
			}
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = App.Database.GetItems ();
		}

		public void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {
			var todoItem = e.SelectedItem as TodoItem;
			var todoPage = new TodoItemXaml();
			todoPage.BindingContext = todoItem;
			Navigation.PushAsync(todoPage);
		}
	}
}