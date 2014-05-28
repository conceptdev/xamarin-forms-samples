using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TodoMvvm
{
	public class TodoListPage : ContentPage
	{
		public TodoListPage ()
		{
			Title = "TodoMvvm";

			NavigationPage.SetHasNavigationBar (this, true);

			var listView = new ListView ();
			listView.RowHeight = 40;
			listView.SetBinding (ListView.ItemsSourceProperty, "Contents");
			listView.SetBinding (ListView.SelectedItemProperty, new Binding ("SelectedItem", BindingMode.TwoWay));
			listView.ItemTemplate = new DataTemplate (typeof (TodoItemCell));

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {listView}
			};

			var tbi = new ToolbarItem ("+", null, () => {
				var t = new TodoItem();
				MessagingCenter.Send (this, "TodoAdd", t);
			}, 0, 0);
			if (Device.OS == TargetPlatform.Android) { // BUG: Android doesn't support the icon being null
				tbi = new ToolbarItem ("+", "plus", () => {
					var t = new TodoItem();
					MessagingCenter.Send (this, "TodoAdd", t);
				}, 0, 0);
			}
			ToolbarItems.Add (tbi);

			if (Device.OS == TargetPlatform.iOS) {
				var tbi2 = new ToolbarItem ("?", null, () => {
					MessagingCenter.Send (this, "TodoListSpeak");
				}, 0, 0);
				ToolbarItems.Add (tbi2);
			}
		}
	}
}

